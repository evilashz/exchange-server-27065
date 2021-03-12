using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Net.ExSmtpClient;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000243 RID: 579
	public class SendMailHelper
	{
		// Token: 0x0600136B RID: 4971 RVA: 0x00039734 File Offset: 0x00037934
		public static void SendMail(string probeName, SmtpProbeWorkDefinition.SendMailDefinition sd, string lamNotificationID, SendMailHelper.CreateMessageStreamDelegate createMessageStreamDelegate = null)
		{
			using (SmtpTalk smtpTalk = new SmtpTalk(new SmtpClientDebugOutput()))
			{
				smtpTalk.Connect(sd.SmtpServer, sd.Port);
				smtpTalk.Ehlo();
				if (!sd.Anonymous)
				{
					smtpTalk.StartTls(true);
					smtpTalk.Ehlo();
					smtpTalk.Authenticate(CredentialCache.DefaultNetworkCredentials, SmtpSspiMechanism.Kerberos);
				}
				string sender;
				if (LocalEndpointManager.IsDataCenter)
				{
					if ((sd.Direction == Directionality.Incoming && !string.IsNullOrEmpty(sd.RecipientTenantID)) || (sd.Direction != Directionality.Incoming && !string.IsNullOrEmpty(sd.SenderTenantID)))
					{
						sender = string.Format("{0} XATTRDIRECT={1} XATTRORGID=xorgid:{2} xsysprobeid={3}", new object[]
						{
							sd.SenderUsername,
							sd.Direction.ToString(),
							(sd.Direction == Directionality.Incoming) ? sd.RecipientTenantID : sd.SenderTenantID,
							lamNotificationID
						});
					}
					else
					{
						sender = string.Format("{0} XATTRDIRECT={1} xsysprobeid={2}", sd.SenderUsername, sd.Direction.ToString(), lamNotificationID);
					}
				}
				else
				{
					sender = string.Format("{0} xsysprobeid={1}", sd.SenderUsername, lamNotificationID);
				}
				smtpTalk.MailFrom(sender, null);
				smtpTalk.RcptTo(sd.RecipientUsername, new bool?(false));
				using (MemoryStream memoryStream = new MemoryStream())
				{
					SendMailHelper.CreateMessageStream(createMessageStreamDelegate, probeName, sd, memoryStream, lamNotificationID);
					memoryStream.Position = 0L;
					smtpTalk.Chunking(memoryStream);
				}
				try
				{
					smtpTalk.Quit();
				}
				catch (SocketException)
				{
				}
				catch (IOException)
				{
				}
			}
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x00039904 File Offset: 0x00037B04
		public static void CreateDefaultMessageStream(string probeName, SmtpProbeWorkDefinition.SendMailDefinition sd, MemoryStream stream, string lamNotificationID)
		{
			string text = Path.Combine(Path.GetTempPath(), string.Format("{0}_{1}", probeName, sd.Message.MessageId));
			try
			{
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				List<string> list = new List<string>(Directory.GetFiles(text, "*.eml"));
				if (list.Count != 1)
				{
					list.ForEach(new Action<string>(File.Delete));
					SendMailHelper.GenerateEML(text, sd, lamNotificationID);
					list = new List<string>(Directory.GetFiles(text, "*.eml"));
				}
				if (list.Count != 1)
				{
					throw new Exception(string.Format("The number of *.eml files under {0} is not 1", text));
				}
				using (FileStream fileStream = File.OpenRead(list[0]))
				{
					fileStream.CopyTo(stream);
					stream.Position = 0L;
				}
				File.Delete(list[0]);
			}
			finally
			{
				if (Directory.Exists(text))
				{
					foreach (string path in Directory.GetFiles(text))
					{
						File.Delete(path);
					}
					Directory.Delete(text);
				}
			}
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00039A2C File Offset: 0x00037C2C
		private static void CreateMessageStream(SendMailHelper.CreateMessageStreamDelegate createMessageStreamDelegate, string probeName, SmtpProbeWorkDefinition.SendMailDefinition sd, MemoryStream stream, string lamNotificationID)
		{
			if (createMessageStreamDelegate != null)
			{
				createMessageStreamDelegate(probeName, sd, stream, lamNotificationID);
				return;
			}
			SendMailHelper.CreateDefaultMessageStream(probeName, sd, stream, lamNotificationID);
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x00039A48 File Offset: 0x00037C48
		private static void GenerateEML(string privateFolder, SmtpProbeWorkDefinition.SendMailDefinition sd, string lamNotificationID)
		{
			Message message = sd.Message;
			MailMessage mail = sd.Message.Mail;
			mail.From = new MailAddress(sd.SenderUsername);
			mail.To.Add(sd.RecipientUsername);
			mail.Headers.Add("X-LAMNotificationId", lamNotificationID);
			foreach (NameValuePair nameValuePair in message.Headers)
			{
				mail.Headers.Add(nameValuePair.Name, nameValuePair.Value);
			}
			foreach (KeyValuePair<string, object> entry in message.Attachments)
			{
				mail.Attachments.Add(Message.CreateMailAttachment(entry));
			}
			mail.Subject = message.Subject;
			mail.Body = message.Body;
			using (SmtpClient smtpClient = new SmtpClient("127.0.0.1", 25))
			{
				smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
				smtpClient.PickupDirectoryLocation = privateFolder;
				smtpClient.Send(mail);
			}
		}

		// Token: 0x02000244 RID: 580
		// (Invoke) Token: 0x06001371 RID: 4977
		public delegate void CreateMessageStreamDelegate(string probeName, SmtpProbeWorkDefinition.SendMailDefinition sendMailDefinition, MemoryStream memoryStream, string lamNotificationID);
	}
}
