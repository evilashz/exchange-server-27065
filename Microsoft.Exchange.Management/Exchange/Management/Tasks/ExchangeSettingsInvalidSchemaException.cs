using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200118C RID: 4492
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExchangeSettingsInvalidSchemaException : ExchangeSettingsException
	{
		// Token: 0x0600B6AD RID: 46765 RVA: 0x002A0405 File Offset: 0x0029E605
		public ExchangeSettingsInvalidSchemaException(string name) : base(Strings.ExchangeSettingsInvalidSchema(name))
		{
			this.name = name;
		}

		// Token: 0x0600B6AE RID: 46766 RVA: 0x002A041A File Offset: 0x0029E61A
		public ExchangeSettingsInvalidSchemaException(string name, Exception innerException) : base(Strings.ExchangeSettingsInvalidSchema(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600B6AF RID: 46767 RVA: 0x002A0430 File Offset: 0x0029E630
		protected ExchangeSettingsInvalidSchemaException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600B6B0 RID: 46768 RVA: 0x002A045A File Offset: 0x0029E65A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x1700399A RID: 14746
		// (get) Token: 0x0600B6B1 RID: 46769 RVA: 0x002A0475 File Offset: 0x0029E675
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04006300 RID: 25344
		private readonly string name;
	}
}
