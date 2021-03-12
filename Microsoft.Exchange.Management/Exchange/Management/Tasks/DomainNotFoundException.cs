using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DF9 RID: 3577
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DomainNotFoundException : LocalizedException
	{
		// Token: 0x0600A4DA RID: 42202 RVA: 0x002850D5 File Offset: 0x002832D5
		public DomainNotFoundException(string dom) : base(Strings.DomainNotFoundException(dom))
		{
			this.dom = dom;
		}

		// Token: 0x0600A4DB RID: 42203 RVA: 0x002850EA File Offset: 0x002832EA
		public DomainNotFoundException(string dom, Exception innerException) : base(Strings.DomainNotFoundException(dom), innerException)
		{
			this.dom = dom;
		}

		// Token: 0x0600A4DC RID: 42204 RVA: 0x00285100 File Offset: 0x00283300
		protected DomainNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dom = (string)info.GetValue("dom", typeof(string));
		}

		// Token: 0x0600A4DD RID: 42205 RVA: 0x0028512A File Offset: 0x0028332A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dom", this.dom);
		}

		// Token: 0x17003613 RID: 13843
		// (get) Token: 0x0600A4DE RID: 42206 RVA: 0x00285145 File Offset: 0x00283345
		public string Dom
		{
			get
			{
				return this.dom;
			}
		}

		// Token: 0x04005F79 RID: 24441
		private readonly string dom;
	}
}
