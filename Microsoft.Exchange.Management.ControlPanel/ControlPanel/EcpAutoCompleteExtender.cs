using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020005B9 RID: 1465
	[ClientScriptResource("EcpAutoCompleteBehavior", "Microsoft.Exchange.Management.ControlPanel.Client.List.js")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[RequiredScript(typeof(ExtenderControlBase))]
	[TargetControlType(typeof(TextBox))]
	public class EcpAutoCompleteExtender : ExtenderControlBase
	{
		// Token: 0x060042BD RID: 17085 RVA: 0x000CB09C File Offset: 0x000C929C
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddProperty("MinimumPrefixLength", this.MinimumPrefixLength);
			descriptor.AddProperty("FixedMinimumPrefixLength", this.FixedMinimumPrefixLength);
			descriptor.AddProperty("CompletionInterval", this.CompletionInterval);
			descriptor.AddProperty("AutoSuggestionPropertyNames", this.AutoSuggestionPropertyNames);
			descriptor.AddProperty("AutoSuggestionPropertyValues", this.AutoSuggestionPropertyValues);
			descriptor.AddComponentProperty("WebServiceMethod", this.WebServiceMethod.ClientID);
		}

		// Token: 0x060042BE RID: 17086 RVA: 0x000CB12A File Offset: 0x000C932A
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.Controls.Add(this.WebServiceMethod);
		}

		// Token: 0x170025E6 RID: 9702
		// (get) Token: 0x060042BF RID: 17087 RVA: 0x000CB143 File Offset: 0x000C9343
		// (set) Token: 0x060042C0 RID: 17088 RVA: 0x000CB151 File Offset: 0x000C9351
		[ClientPropertyName("MinimumPrefixLength")]
		[ExtenderControlProperty]
		public virtual int MinimumPrefixLength
		{
			get
			{
				return base.GetPropertyValue<int>("MinimumPrefixLength", 2);
			}
			set
			{
				base.SetPropertyValue<int>("MinimumPrefixLength", value);
			}
		}

		// Token: 0x170025E7 RID: 9703
		// (get) Token: 0x060042C1 RID: 17089 RVA: 0x000CB15F File Offset: 0x000C935F
		// (set) Token: 0x060042C2 RID: 17090 RVA: 0x000CB16D File Offset: 0x000C936D
		[ExtenderControlProperty]
		[ClientPropertyName("FixedMinimumPrefixLength")]
		public virtual bool FixedMinimumPrefixLength
		{
			get
			{
				return base.GetPropertyValue<bool>("FixedMinimumPrefixLength", false);
			}
			set
			{
				base.SetPropertyValue<bool>("FixedMinimumPrefixLength", value);
			}
		}

		// Token: 0x170025E8 RID: 9704
		// (get) Token: 0x060042C3 RID: 17091 RVA: 0x000CB17B File Offset: 0x000C937B
		// (set) Token: 0x060042C4 RID: 17092 RVA: 0x000CB18D File Offset: 0x000C938D
		[ClientPropertyName("CompletionInterval")]
		[ExtenderControlProperty]
		public virtual int CompletionInterval
		{
			get
			{
				return base.GetPropertyValue<int>("CompletionInterval", 1000);
			}
			set
			{
				base.SetPropertyValue<int>("CompletionInterval", value);
			}
		}

		// Token: 0x170025E9 RID: 9705
		// (get) Token: 0x060042C5 RID: 17093 RVA: 0x000CB19B File Offset: 0x000C939B
		// (set) Token: 0x060042C6 RID: 17094 RVA: 0x000CB1AA File Offset: 0x000C93AA
		[ExtenderControlProperty]
		[ClientPropertyName("CompletionSetCount")]
		public virtual int CompletionSetCount
		{
			get
			{
				return base.GetPropertyValue<int>("CompletionSetCount", 10);
			}
			set
			{
				base.SetPropertyValue<int>("CompletionSetCount", value);
			}
		}

		// Token: 0x170025EA RID: 9706
		// (get) Token: 0x060042C7 RID: 17095 RVA: 0x000CB1B8 File Offset: 0x000C93B8
		// (set) Token: 0x060042C8 RID: 17096 RVA: 0x000CB1CA File Offset: 0x000C93CA
		[ClientPropertyName("AutoSuggestionPropertyNames")]
		[ExtenderControlProperty]
		public virtual string AutoSuggestionPropertyNames
		{
			get
			{
				return base.GetPropertyValue<string>("AutoSuggestionPropertyNames", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("AutoSuggestionPropertyNames", value);
			}
		}

		// Token: 0x170025EB RID: 9707
		// (get) Token: 0x060042C9 RID: 17097 RVA: 0x000CB1D8 File Offset: 0x000C93D8
		// (set) Token: 0x060042CA RID: 17098 RVA: 0x000CB1EA File Offset: 0x000C93EA
		[ClientPropertyName("AutoSuggestionPropertyValues")]
		[ExtenderControlProperty]
		public virtual string AutoSuggestionPropertyValues
		{
			get
			{
				return base.GetPropertyValue<string>("AutoSuggestionPropertyValues", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("AutoSuggestionPropertyValues", value);
			}
		}

		// Token: 0x170025EC RID: 9708
		// (get) Token: 0x060042CB RID: 17099 RVA: 0x000CB1F8 File Offset: 0x000C93F8
		// (set) Token: 0x060042CC RID: 17100 RVA: 0x000CB200 File Offset: 0x000C9400
		[ClientPropertyName("WebServiceMethod")]
		public WebServiceMethod WebServiceMethod { get; set; }
	}
}
