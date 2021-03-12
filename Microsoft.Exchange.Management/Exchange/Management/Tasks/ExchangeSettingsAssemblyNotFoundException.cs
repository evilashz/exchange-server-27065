using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200118E RID: 4494
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExchangeSettingsAssemblyNotFoundException : ExchangeSettingsException
	{
		// Token: 0x0600B6B8 RID: 46776 RVA: 0x002A0549 File Offset: 0x0029E749
		public ExchangeSettingsAssemblyNotFoundException(string name, string path, string type) : base(Strings.ExchangeSettingsAssemblyNotFound(name, path, type))
		{
			this.name = name;
			this.path = path;
			this.type = type;
		}

		// Token: 0x0600B6B9 RID: 46777 RVA: 0x002A056E File Offset: 0x0029E76E
		public ExchangeSettingsAssemblyNotFoundException(string name, string path, string type, Exception innerException) : base(Strings.ExchangeSettingsAssemblyNotFound(name, path, type), innerException)
		{
			this.name = name;
			this.path = path;
			this.type = type;
		}

		// Token: 0x0600B6BA RID: 46778 RVA: 0x002A0598 File Offset: 0x0029E798
		protected ExchangeSettingsAssemblyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.path = (string)info.GetValue("path", typeof(string));
			this.type = (string)info.GetValue("type", typeof(string));
		}

		// Token: 0x0600B6BB RID: 46779 RVA: 0x002A060D File Offset: 0x0029E80D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("path", this.path);
			info.AddValue("type", this.type);
		}

		// Token: 0x1700399D RID: 14749
		// (get) Token: 0x0600B6BC RID: 46780 RVA: 0x002A064A File Offset: 0x0029E84A
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700399E RID: 14750
		// (get) Token: 0x0600B6BD RID: 46781 RVA: 0x002A0652 File Offset: 0x0029E852
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x1700399F RID: 14751
		// (get) Token: 0x0600B6BE RID: 46782 RVA: 0x002A065A File Offset: 0x0029E85A
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04006303 RID: 25347
		private readonly string name;

		// Token: 0x04006304 RID: 25348
		private readonly string path;

		// Token: 0x04006305 RID: 25349
		private readonly string type;
	}
}
