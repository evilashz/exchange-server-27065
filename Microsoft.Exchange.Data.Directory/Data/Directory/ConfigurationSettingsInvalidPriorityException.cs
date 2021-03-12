using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AF5 RID: 2805
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsInvalidPriorityException : ConfigurationSettingsException
	{
		// Token: 0x06008170 RID: 33136 RVA: 0x001A6772 File Offset: 0x001A4972
		public ConfigurationSettingsInvalidPriorityException(int priority) : base(DirectoryStrings.ConfigurationSettingsInvalidPriority(priority))
		{
			this.priority = priority;
		}

		// Token: 0x06008171 RID: 33137 RVA: 0x001A6787 File Offset: 0x001A4987
		public ConfigurationSettingsInvalidPriorityException(int priority, Exception innerException) : base(DirectoryStrings.ConfigurationSettingsInvalidPriority(priority), innerException)
		{
			this.priority = priority;
		}

		// Token: 0x06008172 RID: 33138 RVA: 0x001A679D File Offset: 0x001A499D
		protected ConfigurationSettingsInvalidPriorityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.priority = (int)info.GetValue("priority", typeof(int));
		}

		// Token: 0x06008173 RID: 33139 RVA: 0x001A67C7 File Offset: 0x001A49C7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("priority", this.priority);
		}

		// Token: 0x17002EF3 RID: 12019
		// (get) Token: 0x06008174 RID: 33140 RVA: 0x001A67E2 File Offset: 0x001A49E2
		public int Priority
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x040055CD RID: 21965
		private readonly int priority;
	}
}
