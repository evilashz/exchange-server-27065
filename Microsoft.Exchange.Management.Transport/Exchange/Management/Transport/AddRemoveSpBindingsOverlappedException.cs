using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000163 RID: 355
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class AddRemoveSpBindingsOverlappedException : InvalidCompliancePolicySpBindingException
	{
		// Token: 0x06000EEF RID: 3823 RVA: 0x000359C4 File Offset: 0x00033BC4
		public AddRemoveSpBindingsOverlappedException(string bindings) : base(Strings.ErrorAddRemoveSpBindingsOverlapped(bindings))
		{
			this.bindings = bindings;
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x000359D9 File Offset: 0x00033BD9
		public AddRemoveSpBindingsOverlappedException(string bindings, Exception innerException) : base(Strings.ErrorAddRemoveSpBindingsOverlapped(bindings), innerException)
		{
			this.bindings = bindings;
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x000359EF File Offset: 0x00033BEF
		protected AddRemoveSpBindingsOverlappedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.bindings = (string)info.GetValue("bindings", typeof(string));
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x00035A19 File Offset: 0x00033C19
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("bindings", this.bindings);
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x00035A34 File Offset: 0x00033C34
		public string Bindings
		{
			get
			{
				return this.bindings;
			}
		}

		// Token: 0x0400066F RID: 1647
		private readonly string bindings;
	}
}
