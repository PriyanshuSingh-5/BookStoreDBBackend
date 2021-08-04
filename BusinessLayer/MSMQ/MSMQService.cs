using System;
using BusinessLayer.EmailServices;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BusinessLayer.MSMQ
{
    public class MSMQService
    {
        readonly EmailService emailService;
        readonly MessageQueue queue = new MessageQueue(@".\private$\FunDooNotes");

        public MSMQService(IConfiguration config)
        {
            emailService = new EmailService(config);
            if (!MessageQueue.Exists(queue.Path))
            {
                MessageQueue.Create(queue.Path);
            }
            queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
        }
        /// <summary>
        /// Sends the password reset link to MSMQ 
        /// </summary>
        /// <param name="resetLink">The reset link.</param>
        public void SendPasswordResetLink(ForgetPasswordModel resetLink)
        {
            try
            {
                Message msg = new Message
                {
                    Label = "password reset",
                    Body = JsonConvert.SerializeObject(resetLink)
                };
                queue.Send(msg);
                queue.ReceiveCompleted += Queue_ReceiveCompleted;
                queue.BeginReceive(TimeSpan.FromSeconds(5));
                queue.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void SendOrderEmail(CustomerOrder order) 
        {
            try
            {
                Message msg = new Message
                {
                    Label = "order email",
                    Body = JsonConvert.SerializeObject(order)
                };
                queue.Send(msg);
                queue.ReceiveCompleted += Queue_ReceiveCompleted;
                queue.BeginReceive(TimeSpan.FromSeconds(5));
                queue.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Handles the ReceiveCompleted event of the Queue control.
        /// sends email when message received from queue
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReceiveCompletedEventArgs"/> instance containing the event data.</param>
        void Queue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {          
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                switch (msg.Label)
                {
                    case "order email": 
                    CustomerOrder order = JsonConvert.DeserializeObject<CustomerOrder>(msg.Body.ToString());
                    emailService.SendOrderEmail(order);
                        break;
                    case "password reset":
                    ForgetPasswordModel forgetPassword = JsonConvert.DeserializeObject<ForgetPasswordModel>(msg.Body.ToString());
                        emailService.SendPasswordResetLinkEmail(forgetPassword);
                        break;
                }
               
                
               
                queue.BeginReceive(TimeSpan.FromSeconds(5));
            }
            catch (Exception)
            {

            }
        }
    }
}
