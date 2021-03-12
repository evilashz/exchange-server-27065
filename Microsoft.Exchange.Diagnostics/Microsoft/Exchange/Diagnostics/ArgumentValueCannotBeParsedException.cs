using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200041C RID: 1052
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ArgumentValueCannotBeParsedException : DiagnosticArgumentException
	{
		// Token: 0x06001963 RID: 6499 RVA: 0x0005FA39 File Offset: 0x0005DC39
		public ArgumentValueCannotBeParsedException(string key, string value, string typeName) : base(DiagnosticsResources.ArgumentValueCannotBeParsed(key, value, typeName))
		{
			this.key = key;
			this.value = value;
			this.typeName = typeName;
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0005FA5E File Offset: 0x0005DC5E
		public ArgumentValueCannotBeParsedException(string key, string value, string typeName, Exception innerException) : base(DiagnosticsResources.ArgumentValueCannotBeParsed(key, value, typeName), innerException)
		{
			this.key = key;
			this.value = value;
			this.typeName = typeName;
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0005FA88 File Offset: 0x0005DC88
		protected ArgumentValueCannotBeParsedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.key = (string)info.GetValue("key", typeof(string));
			this.value = (string)info.GetValue("value", typeof(string));
			this.typeName = (string)info.GetValue("typeName", typeof(string));
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0005FAFD File Offset: 0x0005DCFD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("key", this.key);
			info.AddValue("value", this.value);
			info.AddValue("typeName", this.typeName);
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06001967 RID: 6503 RVA: 0x0005FB3A File Offset: 0x0005DD3A
		public string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06001968 RID: 6504 RVA: 0x0005FB42 File Offset: 0x0005DD42
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06001969 RID: 6505 RVA: 0x0005FB4A File Offset: 0x0005DD4A
		public string TypeName
		{
			get
			{
				return this.typeName;
			}
		}

		// Token: 0x04001DEF RID: 7663
		private readonly string key;

		// Token: 0x04001DF0 RID: 7664
		private readonly string value;

		// Token: 0x04001DF1 RID: 7665
		private readonly string typeName;
	}
}
