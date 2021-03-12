using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E94 RID: 3732
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LargeDataLossNotAcceptedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A7BC RID: 42940 RVA: 0x00288EC0 File Offset: 0x002870C0
		public LargeDataLossNotAcceptedPermanentException(string badItemLimitParamName, string badItemLimitValue, string acceptLargeDataLossParamName, string requestorIdentity) : base(Strings.ErrorLargeDataLossNotAccepted(badItemLimitParamName, badItemLimitValue, acceptLargeDataLossParamName, requestorIdentity))
		{
			this.badItemLimitParamName = badItemLimitParamName;
			this.badItemLimitValue = badItemLimitValue;
			this.acceptLargeDataLossParamName = acceptLargeDataLossParamName;
			this.requestorIdentity = requestorIdentity;
		}

		// Token: 0x0600A7BD RID: 42941 RVA: 0x00288EEF File Offset: 0x002870EF
		public LargeDataLossNotAcceptedPermanentException(string badItemLimitParamName, string badItemLimitValue, string acceptLargeDataLossParamName, string requestorIdentity, Exception innerException) : base(Strings.ErrorLargeDataLossNotAccepted(badItemLimitParamName, badItemLimitValue, acceptLargeDataLossParamName, requestorIdentity), innerException)
		{
			this.badItemLimitParamName = badItemLimitParamName;
			this.badItemLimitValue = badItemLimitValue;
			this.acceptLargeDataLossParamName = acceptLargeDataLossParamName;
			this.requestorIdentity = requestorIdentity;
		}

		// Token: 0x0600A7BE RID: 42942 RVA: 0x00288F20 File Offset: 0x00287120
		protected LargeDataLossNotAcceptedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.badItemLimitParamName = (string)info.GetValue("badItemLimitParamName", typeof(string));
			this.badItemLimitValue = (string)info.GetValue("badItemLimitValue", typeof(string));
			this.acceptLargeDataLossParamName = (string)info.GetValue("acceptLargeDataLossParamName", typeof(string));
			this.requestorIdentity = (string)info.GetValue("requestorIdentity", typeof(string));
		}

		// Token: 0x0600A7BF RID: 42943 RVA: 0x00288FB8 File Offset: 0x002871B8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("badItemLimitParamName", this.badItemLimitParamName);
			info.AddValue("badItemLimitValue", this.badItemLimitValue);
			info.AddValue("acceptLargeDataLossParamName", this.acceptLargeDataLossParamName);
			info.AddValue("requestorIdentity", this.requestorIdentity);
		}

		// Token: 0x17003689 RID: 13961
		// (get) Token: 0x0600A7C0 RID: 42944 RVA: 0x00289011 File Offset: 0x00287211
		public string BadItemLimitParamName
		{
			get
			{
				return this.badItemLimitParamName;
			}
		}

		// Token: 0x1700368A RID: 13962
		// (get) Token: 0x0600A7C1 RID: 42945 RVA: 0x00289019 File Offset: 0x00287219
		public string BadItemLimitValue
		{
			get
			{
				return this.badItemLimitValue;
			}
		}

		// Token: 0x1700368B RID: 13963
		// (get) Token: 0x0600A7C2 RID: 42946 RVA: 0x00289021 File Offset: 0x00287221
		public string AcceptLargeDataLossParamName
		{
			get
			{
				return this.acceptLargeDataLossParamName;
			}
		}

		// Token: 0x1700368C RID: 13964
		// (get) Token: 0x0600A7C3 RID: 42947 RVA: 0x00289029 File Offset: 0x00287229
		public string RequestorIdentity
		{
			get
			{
				return this.requestorIdentity;
			}
		}

		// Token: 0x04005FEF RID: 24559
		private readonly string badItemLimitParamName;

		// Token: 0x04005FF0 RID: 24560
		private readonly string badItemLimitValue;

		// Token: 0x04005FF1 RID: 24561
		private readonly string acceptLargeDataLossParamName;

		// Token: 0x04005FF2 RID: 24562
		private readonly string requestorIdentity;
	}
}
