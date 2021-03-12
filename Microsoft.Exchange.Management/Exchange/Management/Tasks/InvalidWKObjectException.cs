using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DF6 RID: 3574
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidWKObjectException : LocalizedException
	{
		// Token: 0x0600A4CA RID: 42186 RVA: 0x00284F0D File Offset: 0x0028310D
		public InvalidWKObjectException(string wkentry, string container) : base(Strings.InvalidWKObjectException(wkentry, container))
		{
			this.wkentry = wkentry;
			this.container = container;
		}

		// Token: 0x0600A4CB RID: 42187 RVA: 0x00284F2A File Offset: 0x0028312A
		public InvalidWKObjectException(string wkentry, string container, Exception innerException) : base(Strings.InvalidWKObjectException(wkentry, container), innerException)
		{
			this.wkentry = wkentry;
			this.container = container;
		}

		// Token: 0x0600A4CC RID: 42188 RVA: 0x00284F48 File Offset: 0x00283148
		protected InvalidWKObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.wkentry = (string)info.GetValue("wkentry", typeof(string));
			this.container = (string)info.GetValue("container", typeof(string));
		}

		// Token: 0x0600A4CD RID: 42189 RVA: 0x00284F9D File Offset: 0x0028319D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("wkentry", this.wkentry);
			info.AddValue("container", this.container);
		}

		// Token: 0x1700360F RID: 13839
		// (get) Token: 0x0600A4CE RID: 42190 RVA: 0x00284FC9 File Offset: 0x002831C9
		public string Wkentry
		{
			get
			{
				return this.wkentry;
			}
		}

		// Token: 0x17003610 RID: 13840
		// (get) Token: 0x0600A4CF RID: 42191 RVA: 0x00284FD1 File Offset: 0x002831D1
		public string Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x04005F75 RID: 24437
		private readonly string wkentry;

		// Token: 0x04005F76 RID: 24438
		private readonly string container;
	}
}
