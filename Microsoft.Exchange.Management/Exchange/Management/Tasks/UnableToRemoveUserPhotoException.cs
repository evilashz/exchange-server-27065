using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001000 RID: 4096
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToRemoveUserPhotoException : LocalizedException
	{
		// Token: 0x0600AEBC RID: 44732 RVA: 0x0029352A File Offset: 0x0029172A
		public UnableToRemoveUserPhotoException(string identity, string failure) : base(Strings.UnableToRemoveUserPhotoException(identity, failure))
		{
			this.identity = identity;
			this.failure = failure;
		}

		// Token: 0x0600AEBD RID: 44733 RVA: 0x00293547 File Offset: 0x00291747
		public UnableToRemoveUserPhotoException(string identity, string failure, Exception innerException) : base(Strings.UnableToRemoveUserPhotoException(identity, failure), innerException)
		{
			this.identity = identity;
			this.failure = failure;
		}

		// Token: 0x0600AEBE RID: 44734 RVA: 0x00293568 File Offset: 0x00291768
		protected UnableToRemoveUserPhotoException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
			this.failure = (string)info.GetValue("failure", typeof(string));
		}

		// Token: 0x0600AEBF RID: 44735 RVA: 0x002935BD File Offset: 0x002917BD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
			info.AddValue("failure", this.failure);
		}

		// Token: 0x170037D9 RID: 14297
		// (get) Token: 0x0600AEC0 RID: 44736 RVA: 0x002935E9 File Offset: 0x002917E9
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x170037DA RID: 14298
		// (get) Token: 0x0600AEC1 RID: 44737 RVA: 0x002935F1 File Offset: 0x002917F1
		public string Failure
		{
			get
			{
				return this.failure;
			}
		}

		// Token: 0x0400613F RID: 24895
		private readonly string identity;

		// Token: 0x04006140 RID: 24896
		private readonly string failure;
	}
}
