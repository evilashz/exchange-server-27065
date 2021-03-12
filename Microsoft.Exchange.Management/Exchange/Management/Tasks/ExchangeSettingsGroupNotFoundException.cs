using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001186 RID: 4486
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExchangeSettingsGroupNotFoundException : ExchangeSettingsException
	{
		// Token: 0x0600B68F RID: 46735 RVA: 0x002A0128 File Offset: 0x0029E328
		public ExchangeSettingsGroupNotFoundException(string name) : base(Strings.ExchangeSettingsGroupNotFound(name))
		{
			this.name = name;
		}

		// Token: 0x0600B690 RID: 46736 RVA: 0x002A013D File Offset: 0x0029E33D
		public ExchangeSettingsGroupNotFoundException(string name, Exception innerException) : base(Strings.ExchangeSettingsGroupNotFound(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600B691 RID: 46737 RVA: 0x002A0153 File Offset: 0x0029E353
		protected ExchangeSettingsGroupNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600B692 RID: 46738 RVA: 0x002A017D File Offset: 0x0029E37D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x17003994 RID: 14740
		// (get) Token: 0x0600B693 RID: 46739 RVA: 0x002A0198 File Offset: 0x0029E398
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040062FA RID: 25338
		private readonly string name;
	}
}
