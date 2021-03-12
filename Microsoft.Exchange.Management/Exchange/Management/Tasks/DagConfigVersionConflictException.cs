using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001096 RID: 4246
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagConfigVersionConflictException : LocalizedException
	{
		// Token: 0x0600B1EC RID: 45548 RVA: 0x00299169 File Offset: 0x00297369
		public DagConfigVersionConflictException(string dagConfigName, int dagconfigversion, int xmlversion) : base(Strings.DagConfigVersionConflictException(dagConfigName, dagconfigversion, xmlversion))
		{
			this.dagConfigName = dagConfigName;
			this.dagconfigversion = dagconfigversion;
			this.xmlversion = xmlversion;
		}

		// Token: 0x0600B1ED RID: 45549 RVA: 0x0029918E File Offset: 0x0029738E
		public DagConfigVersionConflictException(string dagConfigName, int dagconfigversion, int xmlversion, Exception innerException) : base(Strings.DagConfigVersionConflictException(dagConfigName, dagconfigversion, xmlversion), innerException)
		{
			this.dagConfigName = dagConfigName;
			this.dagconfigversion = dagconfigversion;
			this.xmlversion = xmlversion;
		}

		// Token: 0x0600B1EE RID: 45550 RVA: 0x002991B8 File Offset: 0x002973B8
		protected DagConfigVersionConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dagConfigName = (string)info.GetValue("dagConfigName", typeof(string));
			this.dagconfigversion = (int)info.GetValue("dagconfigversion", typeof(int));
			this.xmlversion = (int)info.GetValue("xmlversion", typeof(int));
		}

		// Token: 0x0600B1EF RID: 45551 RVA: 0x0029922D File Offset: 0x0029742D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dagConfigName", this.dagConfigName);
			info.AddValue("dagconfigversion", this.dagconfigversion);
			info.AddValue("xmlversion", this.xmlversion);
		}

		// Token: 0x170038B1 RID: 14513
		// (get) Token: 0x0600B1F0 RID: 45552 RVA: 0x0029926A File Offset: 0x0029746A
		public string DagConfigName
		{
			get
			{
				return this.dagConfigName;
			}
		}

		// Token: 0x170038B2 RID: 14514
		// (get) Token: 0x0600B1F1 RID: 45553 RVA: 0x00299272 File Offset: 0x00297472
		public int Dagconfigversion
		{
			get
			{
				return this.dagconfigversion;
			}
		}

		// Token: 0x170038B3 RID: 14515
		// (get) Token: 0x0600B1F2 RID: 45554 RVA: 0x0029927A File Offset: 0x0029747A
		public int Xmlversion
		{
			get
			{
				return this.xmlversion;
			}
		}

		// Token: 0x04006217 RID: 25111
		private readonly string dagConfigName;

		// Token: 0x04006218 RID: 25112
		private readonly int dagconfigversion;

		// Token: 0x04006219 RID: 25113
		private readonly int xmlversion;
	}
}
