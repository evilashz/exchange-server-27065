using System;
using System.Xml;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001C1 RID: 449
	internal class RecordConfig : ActivityConfig
	{
		// Token: 0x06000D0B RID: 3339 RVA: 0x0003995A File Offset: 0x00037B5A
		internal RecordConfig(ActivityManagerConfig manager) : base(manager)
		{
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00039963 File Offset: 0x00037B63
		internal string DtmfStopTones
		{
			get
			{
				return this.dtmfStopTones;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x0003996B File Offset: 0x00037B6B
		internal string Type
		{
			get
			{
				return this.recType;
			}
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00039973 File Offset: 0x00037B73
		internal override ActivityBase CreateActivity(ActivityManager manager)
		{
			return new Record(manager, this);
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0003997C File Offset: 0x00037B7C
		protected override void LoadAttributes(XmlNode rootNode)
		{
			base.LoadAttributes(rootNode);
			this.dtmfStopTones = rootNode.Attributes["dtmfStopTones"].Value;
			this.recType = string.Intern(rootNode.Attributes["type"].Value);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x000399CC File Offset: 0x00037BCC
		protected override void LoadComplete()
		{
			if (!ActivityConfig.TransitionMap.ContainsKey(ActivityConfig.BuildTransitionMapKey(this, "anyKey")) || !ActivityConfig.TransitionMap.ContainsKey(ActivityConfig.BuildTransitionMapKey(this, "timeout")) || !ActivityConfig.TransitionMap.ContainsKey(ActivityConfig.BuildTransitionMapKey(this, "silence")))
			{
				throw new FsmConfigurationException(Strings.RecordMissingTransitions(base.ActivityId));
			}
		}

		// Token: 0x04000A69 RID: 2665
		private string dtmfStopTones;

		// Token: 0x04000A6A RID: 2666
		private string recType;
	}
}
