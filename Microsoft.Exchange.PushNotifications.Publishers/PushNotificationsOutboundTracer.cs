using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000C9 RID: 201
	internal class PushNotificationsOutboundTracer : IOutboundTracer
	{
		// Token: 0x060006AC RID: 1708 RVA: 0x000154A4 File Offset: 0x000136A4
		public PushNotificationsOutboundTracer(string tracerId)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("tracerId", tracerId);
			this.TracerId = tracerId;
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x000154BE File Offset: 0x000136BE
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x000154C6 File Offset: 0x000136C6
		private string TracerId { get; set; }

		// Token: 0x060006AF RID: 1711 RVA: 0x000154CF File Offset: 0x000136CF
		public void LogError(int hashCode, string formatString, params object[] args)
		{
			PushNotificationsCrimsonEvents.OutboundTracerError.Log<string, string>(this.TracerId, this.GenerateMessage(formatString, args));
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x000154E9 File Offset: 0x000136E9
		public void LogInformation(int hashCode, string formatString, params object[] args)
		{
			if (PushNotificationsCrimsonEvents.OutboundTracerInformation.IsEnabled(PushNotificationsCrimsonEvent.Provider))
			{
				PushNotificationsCrimsonEvents.OutboundTracerInformation.Log<string, string>(this.TracerId, this.GenerateMessage(hashCode, formatString, args));
			}
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00015515 File Offset: 0x00013715
		public void LogToken(int hashCode, string tokenString)
		{
			if (PushNotificationsCrimsonEvents.OutboundTracerInformation.IsEnabled(PushNotificationsCrimsonEvent.Provider))
			{
				PushNotificationsCrimsonEvents.OutboundTracerInformation.Log<string, string>(this.TracerId, string.Format("Token:{0}", tokenString));
			}
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00015543 File Offset: 0x00013743
		public void LogWarning(int hashCode, string formatString, params object[] args)
		{
			PushNotificationsCrimsonEvents.OutboundTracerWarning.Log<string, string>(this.TracerId, this.GenerateMessage(formatString, args));
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001555D File Offset: 0x0001375D
		private string GenerateMessage(int hashCode, string formatString, params object[] args)
		{
			return string.Format("[{0}] {1}", hashCode, this.GenerateMessage(formatString, args));
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00015578 File Offset: 0x00013778
		private string GenerateMessage(string formatString, params object[] args)
		{
			string result;
			try
			{
				if (args == null || args.Length == 0)
				{
					result = formatString;
				}
				else
				{
					result = string.Format(formatString, args);
				}
			}
			catch (FormatException)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object arg in args)
				{
					stringBuilder.AppendFormat("{0};", arg);
				}
				result = string.Format("{0}, args:[{1}]", formatString, stringBuilder.ToString());
			}
			return result;
		}
	}
}
