using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DF1 RID: 3569
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnwillingToPerformException : LocalizedException
	{
		// Token: 0x0600A4AE RID: 42158 RVA: 0x00284BAF File Offset: 0x00282DAF
		public UnwillingToPerformException(string name, string dom) : base(Strings.UnwillingToPerformException(name, dom))
		{
			this.name = name;
			this.dom = dom;
		}

		// Token: 0x0600A4AF RID: 42159 RVA: 0x00284BCC File Offset: 0x00282DCC
		public UnwillingToPerformException(string name, string dom, Exception innerException) : base(Strings.UnwillingToPerformException(name, dom), innerException)
		{
			this.name = name;
			this.dom = dom;
		}

		// Token: 0x0600A4B0 RID: 42160 RVA: 0x00284BEC File Offset: 0x00282DEC
		protected UnwillingToPerformException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.dom = (string)info.GetValue("dom", typeof(string));
		}

		// Token: 0x0600A4B1 RID: 42161 RVA: 0x00284C41 File Offset: 0x00282E41
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("dom", this.dom);
		}

		// Token: 0x17003607 RID: 13831
		// (get) Token: 0x0600A4B2 RID: 42162 RVA: 0x00284C6D File Offset: 0x00282E6D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17003608 RID: 13832
		// (get) Token: 0x0600A4B3 RID: 42163 RVA: 0x00284C75 File Offset: 0x00282E75
		public string Dom
		{
			get
			{
				return this.dom;
			}
		}

		// Token: 0x04005F6D RID: 24429
		private readonly string name;

		// Token: 0x04005F6E RID: 24430
		private readonly string dom;
	}
}
