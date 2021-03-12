using System;
using System.Configuration;
using Microsoft.Exchange.Transport.Extensibility;

namespace Microsoft.Exchange.Transport.Configuration
{
	// Token: 0x0200028F RID: 655
	internal class AgentErrorHandlingOverrideSection : ConfigurationSection
	{
		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001C15 RID: 7189 RVA: 0x00073031 File Offset: 0x00071231
		[ConfigurationProperty("", IsDefaultCollection = true, IsRequired = true)]
		public AgentErrorHandlingOverrideSection.OverrideList Overrides
		{
			get
			{
				return (AgentErrorHandlingOverrideSection.OverrideList)base[""];
			}
		}

		// Token: 0x02000290 RID: 656
		public class Override : ConfigurationElement
		{
			// Token: 0x17000757 RID: 1879
			// (get) Token: 0x06001C17 RID: 7191 RVA: 0x0007304B File Offset: 0x0007124B
			[ConfigurationProperty("name", IsRequired = true)]
			public string Name
			{
				get
				{
					return (string)base["name"];
				}
			}

			// Token: 0x17000758 RID: 1880
			// (get) Token: 0x06001C18 RID: 7192 RVA: 0x0007305D File Offset: 0x0007125D
			[ConfigurationProperty("condition", IsRequired = true)]
			public AgentErrorHandlingOverrideSection.Condition Condition
			{
				get
				{
					return (AgentErrorHandlingOverrideSection.Condition)base["condition"];
				}
			}

			// Token: 0x17000759 RID: 1881
			// (get) Token: 0x06001C19 RID: 7193 RVA: 0x0007306F File Offset: 0x0007126F
			[ConfigurationProperty("action", IsRequired = true)]
			public AgentErrorHandlingOverrideSection.Action Action
			{
				get
				{
					return (AgentErrorHandlingOverrideSection.Action)base["action"];
				}
			}
		}

		// Token: 0x02000291 RID: 657
		internal class Condition : ConfigurationElement
		{
			// Token: 0x1700075A RID: 1882
			// (get) Token: 0x06001C1B RID: 7195 RVA: 0x00073089 File Offset: 0x00071289
			[ConfigurationProperty("contextId", IsRequired = true)]
			public string ContextId
			{
				get
				{
					return (string)base["contextId"];
				}
			}

			// Token: 0x1700075B RID: 1883
			// (get) Token: 0x06001C1C RID: 7196 RVA: 0x0007309B File Offset: 0x0007129B
			[ConfigurationProperty("exceptionType")]
			public string ExceptionType
			{
				get
				{
					return (string)base["exceptionType"];
				}
			}

			// Token: 0x1700075C RID: 1884
			// (get) Token: 0x06001C1D RID: 7197 RVA: 0x000730AD File Offset: 0x000712AD
			[ConfigurationProperty("exceptionMessage")]
			public string ExceptionMessage
			{
				get
				{
					return (string)base["exceptionMessage"];
				}
			}

			// Token: 0x1700075D RID: 1885
			// (get) Token: 0x06001C1E RID: 7198 RVA: 0x000730BF File Offset: 0x000712BF
			[ConfigurationProperty("deferCount", DefaultValue = 0)]
			public int DeferCount
			{
				get
				{
					return (int)base["deferCount"];
				}
			}
		}

		// Token: 0x02000292 RID: 658
		internal class Action : ConfigurationElement
		{
			// Token: 0x1700075E RID: 1886
			// (get) Token: 0x06001C20 RID: 7200 RVA: 0x000730D9 File Offset: 0x000712D9
			[ConfigurationProperty("type", IsRequired = true)]
			public ErrorHandlingActionType ActionType
			{
				get
				{
					return (ErrorHandlingActionType)base["type"];
				}
			}

			// Token: 0x1700075F RID: 1887
			// (get) Token: 0x06001C21 RID: 7201 RVA: 0x000730EB File Offset: 0x000712EB
			[ConfigurationProperty("message")]
			public string NDRMessage
			{
				get
				{
					return (string)base["message"];
				}
			}

			// Token: 0x17000760 RID: 1888
			// (get) Token: 0x06001C22 RID: 7202 RVA: 0x000730FD File Offset: 0x000712FD
			[ConfigurationProperty("statusCode")]
			public string NDRStatusCode
			{
				get
				{
					return (string)base["statusCode"];
				}
			}

			// Token: 0x17000761 RID: 1889
			// (get) Token: 0x06001C23 RID: 7203 RVA: 0x0007310F File Offset: 0x0007130F
			[ConfigurationProperty("enhancedStatusCode")]
			public string NDREnhancedStatusCode
			{
				get
				{
					return (string)base["enhancedStatusCode"];
				}
			}

			// Token: 0x17000762 RID: 1890
			// (get) Token: 0x06001C24 RID: 7204 RVA: 0x00073121 File Offset: 0x00071321
			[ConfigurationProperty("statusText")]
			public string NDRStatusText
			{
				get
				{
					return (string)base["statusText"];
				}
			}

			// Token: 0x17000763 RID: 1891
			// (get) Token: 0x06001C25 RID: 7205 RVA: 0x00073133 File Offset: 0x00071333
			[ConfigurationProperty("interval")]
			public TimeSpan DeferInterval
			{
				get
				{
					return (TimeSpan)base["interval"];
				}
			}

			// Token: 0x17000764 RID: 1892
			// (get) Token: 0x06001C26 RID: 7206 RVA: 0x00073145 File Offset: 0x00071345
			[ConfigurationProperty("isIntervalProgressive", DefaultValue = false)]
			public bool IsIntervalProgressive
			{
				get
				{
					return (bool)base["isIntervalProgressive"];
				}
			}
		}

		// Token: 0x02000293 RID: 659
		internal class OverrideList : ConfigurationElementCollection
		{
			// Token: 0x17000765 RID: 1893
			public AgentErrorHandlingOverrideSection.Override this[int index]
			{
				get
				{
					return (AgentErrorHandlingOverrideSection.Override)base.BaseGet(index);
				}
			}

			// Token: 0x06001C29 RID: 7209 RVA: 0x0007316D File Offset: 0x0007136D
			protected override ConfigurationElement CreateNewElement()
			{
				return new AgentErrorHandlingOverrideSection.Override();
			}

			// Token: 0x06001C2A RID: 7210 RVA: 0x00073174 File Offset: 0x00071374
			protected override object GetElementKey(ConfigurationElement element)
			{
				return ((AgentErrorHandlingOverrideSection.Override)element).Name;
			}

			// Token: 0x17000766 RID: 1894
			// (get) Token: 0x06001C2B RID: 7211 RVA: 0x00073181 File Offset: 0x00071381
			public override ConfigurationElementCollectionType CollectionType
			{
				get
				{
					return ConfigurationElementCollectionType.BasicMap;
				}
			}

			// Token: 0x17000767 RID: 1895
			// (get) Token: 0x06001C2C RID: 7212 RVA: 0x00073184 File Offset: 0x00071384
			protected override string ElementName
			{
				get
				{
					return "override";
				}
			}
		}
	}
}
