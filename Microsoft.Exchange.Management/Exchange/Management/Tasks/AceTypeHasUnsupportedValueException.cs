using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E15 RID: 3605
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AceTypeHasUnsupportedValueException : LocalizedException
	{
		// Token: 0x0600A56C RID: 42348 RVA: 0x00286051 File Offset: 0x00284251
		public AceTypeHasUnsupportedValueException(string aceType) : base(Strings.AceTypeHasUnsupportedValueException(aceType))
		{
			this.aceType = aceType;
		}

		// Token: 0x0600A56D RID: 42349 RVA: 0x00286066 File Offset: 0x00284266
		public AceTypeHasUnsupportedValueException(string aceType, Exception innerException) : base(Strings.AceTypeHasUnsupportedValueException(aceType), innerException)
		{
			this.aceType = aceType;
		}

		// Token: 0x0600A56E RID: 42350 RVA: 0x0028607C File Offset: 0x0028427C
		protected AceTypeHasUnsupportedValueException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.aceType = (string)info.GetValue("aceType", typeof(string));
		}

		// Token: 0x0600A56F RID: 42351 RVA: 0x002860A6 File Offset: 0x002842A6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("aceType", this.aceType);
		}

		// Token: 0x17003635 RID: 13877
		// (get) Token: 0x0600A570 RID: 42352 RVA: 0x002860C1 File Offset: 0x002842C1
		public string AceType
		{
			get
			{
				return this.aceType;
			}
		}

		// Token: 0x04005F9B RID: 24475
		private readonly string aceType;
	}
}
