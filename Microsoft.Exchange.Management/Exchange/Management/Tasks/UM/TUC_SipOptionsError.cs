using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011F0 RID: 4592
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TUC_SipOptionsError : LocalizedException
	{
		// Token: 0x0600B98C RID: 47500 RVA: 0x002A617D File Offset: 0x002A437D
		public TUC_SipOptionsError(string targetUri, string error) : base(Strings.SipOptionsError(targetUri, error))
		{
			this.targetUri = targetUri;
			this.error = error;
		}

		// Token: 0x0600B98D RID: 47501 RVA: 0x002A619A File Offset: 0x002A439A
		public TUC_SipOptionsError(string targetUri, string error, Exception innerException) : base(Strings.SipOptionsError(targetUri, error), innerException)
		{
			this.targetUri = targetUri;
			this.error = error;
		}

		// Token: 0x0600B98E RID: 47502 RVA: 0x002A61B8 File Offset: 0x002A43B8
		protected TUC_SipOptionsError(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.targetUri = (string)info.GetValue("targetUri", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600B98F RID: 47503 RVA: 0x002A620D File Offset: 0x002A440D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("targetUri", this.targetUri);
			info.AddValue("error", this.error);
		}

		// Token: 0x17003A49 RID: 14921
		// (get) Token: 0x0600B990 RID: 47504 RVA: 0x002A6239 File Offset: 0x002A4439
		public string TargetUri
		{
			get
			{
				return this.targetUri;
			}
		}

		// Token: 0x17003A4A RID: 14922
		// (get) Token: 0x0600B991 RID: 47505 RVA: 0x002A6241 File Offset: 0x002A4441
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04006464 RID: 25700
		private readonly string targetUri;

		// Token: 0x04006465 RID: 25701
		private readonly string error;
	}
}
