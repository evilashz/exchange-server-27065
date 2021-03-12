using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AFE RID: 2814
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsInvalidMatchException : ConfigurationSettingsException
	{
		// Token: 0x0600819F RID: 33183 RVA: 0x001A6C55 File Offset: 0x001A4E55
		public ConfigurationSettingsInvalidMatchException(string expression) : base(DirectoryStrings.ConfigurationSettingsInvalidMatch(expression))
		{
			this.expression = expression;
		}

		// Token: 0x060081A0 RID: 33184 RVA: 0x001A6C6A File Offset: 0x001A4E6A
		public ConfigurationSettingsInvalidMatchException(string expression, Exception innerException) : base(DirectoryStrings.ConfigurationSettingsInvalidMatch(expression), innerException)
		{
			this.expression = expression;
		}

		// Token: 0x060081A1 RID: 33185 RVA: 0x001A6C80 File Offset: 0x001A4E80
		protected ConfigurationSettingsInvalidMatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.expression = (string)info.GetValue("expression", typeof(string));
		}

		// Token: 0x060081A2 RID: 33186 RVA: 0x001A6CAA File Offset: 0x001A4EAA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("expression", this.expression);
		}

		// Token: 0x17002EFE RID: 12030
		// (get) Token: 0x060081A3 RID: 33187 RVA: 0x001A6CC5 File Offset: 0x001A4EC5
		public string Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x040055D8 RID: 21976
		private readonly string expression;
	}
}
