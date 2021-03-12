using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001189 RID: 4489
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExchangeSettingsGroupAlreadyExistsException : ExchangeSettingsException
	{
		// Token: 0x0600B69D RID: 46749 RVA: 0x002A0247 File Offset: 0x0029E447
		public ExchangeSettingsGroupAlreadyExistsException(string name) : base(Strings.ExchangeSettingsGroupAlreadyExists(name))
		{
			this.name = name;
		}

		// Token: 0x0600B69E RID: 46750 RVA: 0x002A025C File Offset: 0x0029E45C
		public ExchangeSettingsGroupAlreadyExistsException(string name, Exception innerException) : base(Strings.ExchangeSettingsGroupAlreadyExists(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600B69F RID: 46751 RVA: 0x002A0272 File Offset: 0x0029E472
		protected ExchangeSettingsGroupAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600B6A0 RID: 46752 RVA: 0x002A029C File Offset: 0x0029E49C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x17003996 RID: 14742
		// (get) Token: 0x0600B6A1 RID: 46753 RVA: 0x002A02B7 File Offset: 0x0029E4B7
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040062FC RID: 25340
		private readonly string name;
	}
}
