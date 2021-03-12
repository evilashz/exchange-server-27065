using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001F1 RID: 497
	public class OwaQueryStringParameters : QueryStringParameters
	{
		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x000646D4 File Offset: 0x000628D4
		// (set) Token: 0x0600102A RID: 4138 RVA: 0x000646F0 File Offset: 0x000628F0
		public ApplicationElement ApplicationElement
		{
			get
			{
				return (ApplicationElement)FormsRegistry.ApplicationElementParser.Parse(base.GetValue("ae"));
			}
			set
			{
				base["ae"] = value.ToString();
			}
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x00064708 File Offset: 0x00062908
		public void SetApplicationElement(string value)
		{
			base["ae"] = value;
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x00064716 File Offset: 0x00062916
		// (set) Token: 0x0600102D RID: 4141 RVA: 0x00064723 File Offset: 0x00062923
		public string ItemClass
		{
			get
			{
				return base.GetValue("t");
			}
			set
			{
				base["t"] = value;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x00064731 File Offset: 0x00062931
		// (set) Token: 0x0600102F RID: 4143 RVA: 0x0006473E File Offset: 0x0006293E
		public string Action
		{
			get
			{
				return base.GetValue("a");
			}
			set
			{
				base["a"] = value;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x0006474C File Offset: 0x0006294C
		// (set) Token: 0x06001031 RID: 4145 RVA: 0x00064759 File Offset: 0x00062959
		public string State
		{
			get
			{
				return base.GetValue("s");
			}
			set
			{
				base["s"] = value;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x00064767 File Offset: 0x00062967
		// (set) Token: 0x06001033 RID: 4147 RVA: 0x00064774 File Offset: 0x00062974
		public string Id
		{
			get
			{
				return base.GetValue("id");
			}
			set
			{
				base["id"] = value;
			}
		}

		// Token: 0x04000ADB RID: 2779
		private const string ApplicationElementParameter = "ae";

		// Token: 0x04000ADC RID: 2780
		private const string ItemClassParameter = "t";

		// Token: 0x04000ADD RID: 2781
		private const string ActionParameter = "a";

		// Token: 0x04000ADE RID: 2782
		private const string StateParameter = "s";

		// Token: 0x04000ADF RID: 2783
		private const string IdParameter = "id";
	}
}
