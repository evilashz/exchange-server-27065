using System;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x0200000D RID: 13
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	public class PushNotificationFault : IExtensibleDataObject
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00002D2C File Offset: 0x00000F2C
		public PushNotificationFault(Exception ex) : this(ex, false, 0, true)
		{
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002D38 File Offset: 0x00000F38
		public PushNotificationFault(Exception ex, int backOffTimeInMilliseconds, bool includeOriginatingServer) : this(ex, false, backOffTimeInMilliseconds, includeOriginatingServer)
		{
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002D44 File Offset: 0x00000F44
		public PushNotificationFault(Exception ex, bool includeExceptionDetails, int backOffTimeInMilliseconds = 0, bool includeOriginatingServer = true)
		{
			ArgumentValidator.ThrowIfNull("ex", ex);
			if (ex is LocalizedException)
			{
				this.Message = ((LocalizedException)ex).LocalizedString;
			}
			else
			{
				this.Message = ex.Message;
			}
			this.CanRetry = (ex is PushNotificationTransientException);
			if (includeExceptionDetails)
			{
				this.StackTrace = ex.StackTrace;
				if (ex.InnerException != null)
				{
					this.InnerFault = new PushNotificationFault(ex.InnerException, includeExceptionDetails, 0, true);
				}
			}
			this.BackOffTimeInMilliseconds = backOffTimeInMilliseconds;
			if (includeOriginatingServer)
			{
				this.OriginatingServer = Environment.MachineName;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002DDE File Offset: 0x00000FDE
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002DE6 File Offset: 0x00000FE6
		[DataMember]
		public string Message { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002DEF File Offset: 0x00000FEF
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002DF7 File Offset: 0x00000FF7
		[DataMember]
		public bool CanRetry { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002E00 File Offset: 0x00001000
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002E08 File Offset: 0x00001008
		[DataMember(EmitDefaultValue = false)]
		public string StackTrace { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002E11 File Offset: 0x00001011
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002E19 File Offset: 0x00001019
		[DataMember(EmitDefaultValue = false)]
		public PushNotificationFault InnerFault { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002E22 File Offset: 0x00001022
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002E2A File Offset: 0x0000102A
		[DataMember(EmitDefaultValue = false)]
		public int BackOffTimeInMilliseconds { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002E33 File Offset: 0x00001033
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002E3B File Offset: 0x0000103B
		[DataMember(EmitDefaultValue = false)]
		public string OriginatingServer { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002E44 File Offset: 0x00001044
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002E4C File Offset: 0x0000104C
		public ExtensionDataObject ExtensionData { get; set; }

		// Token: 0x06000066 RID: 102 RVA: 0x00002E55 File Offset: 0x00001055
		public override string ToString()
		{
			return this.Message;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002E60 File Offset: 0x00001060
		public string ToFullString()
		{
			if (this.toFullStringCache == null)
			{
				StringBuilder stringBuilder = new StringBuilder("{");
				PushNotificationFault.ExceptionToFullString(this, ref stringBuilder);
				if (this.InnerFault != null)
				{
					stringBuilder.Append("\",InnerException\":{");
					PushNotificationFault.ExceptionToFullString(this.InnerFault, ref stringBuilder);
					stringBuilder.Append("}");
				}
				if (this.BackOffTimeInMilliseconds != 0)
				{
					stringBuilder.AppendFormat(",\"BackOffTimeInMilliseconds\":{0}", this.BackOffTimeInMilliseconds);
				}
				if (!string.IsNullOrEmpty(this.OriginatingServer))
				{
					stringBuilder.AppendFormat(",\"OriginatingServer\":{0}", this.OriginatingServer);
				}
				stringBuilder.Append("}");
				this.toFullStringCache = stringBuilder.ToString();
			}
			return this.toFullStringCache;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002F14 File Offset: 0x00001114
		private static void ExceptionToFullString(PushNotificationFault notificationFault, ref StringBuilder builder)
		{
			builder.AppendFormat("\"Message\":{0},\"CanRetry\":{1}", notificationFault.Message, notificationFault.CanRetry);
			if (!string.IsNullOrEmpty(notificationFault.StackTrace))
			{
				builder.AppendFormat(",\"StackTrace\":{0}", notificationFault.StackTrace);
			}
		}

		// Token: 0x0400001E RID: 30
		private string toFullStringCache;
	}
}
