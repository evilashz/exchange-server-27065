using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EAF RID: 3759
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MRSCorruptDataInDiagnosticsException : MRSDiagnosticQueryException
	{
		// Token: 0x0600A840 RID: 43072 RVA: 0x00289B05 File Offset: 0x00287D05
		public MRSCorruptDataInDiagnosticsException(string element, string value) : base(Strings.MRSCorruptDataInDiagnostics(element, value))
		{
			this.element = element;
			this.value = value;
		}

		// Token: 0x0600A841 RID: 43073 RVA: 0x00289B22 File Offset: 0x00287D22
		public MRSCorruptDataInDiagnosticsException(string element, string value, Exception innerException) : base(Strings.MRSCorruptDataInDiagnostics(element, value), innerException)
		{
			this.element = element;
			this.value = value;
		}

		// Token: 0x0600A842 RID: 43074 RVA: 0x00289B40 File Offset: 0x00287D40
		protected MRSCorruptDataInDiagnosticsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.element = (string)info.GetValue("element", typeof(string));
			this.value = (string)info.GetValue("value", typeof(string));
		}

		// Token: 0x0600A843 RID: 43075 RVA: 0x00289B95 File Offset: 0x00287D95
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("element", this.element);
			info.AddValue("value", this.value);
		}

		// Token: 0x170036A1 RID: 13985
		// (get) Token: 0x0600A844 RID: 43076 RVA: 0x00289BC1 File Offset: 0x00287DC1
		public string Element
		{
			get
			{
				return this.element;
			}
		}

		// Token: 0x170036A2 RID: 13986
		// (get) Token: 0x0600A845 RID: 43077 RVA: 0x00289BC9 File Offset: 0x00287DC9
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04006007 RID: 24583
		private readonly string element;

		// Token: 0x04006008 RID: 24584
		private readonly string value;
	}
}
