using System;
using System.ComponentModel;
using System.Configuration;
using AjaxControlToolkit;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200058C RID: 1420
	public class StaticBinding : Binding, ISupportServerSideEvaluate
	{
		// Token: 0x17002568 RID: 9576
		// (get) Token: 0x0600418E RID: 16782 RVA: 0x000C7C21 File Offset: 0x000C5E21
		// (set) Token: 0x0600418F RID: 16783 RVA: 0x000C7C29 File Offset: 0x000C5E29
		public virtual object Value { get; set; }

		// Token: 0x17002569 RID: 9577
		// (get) Token: 0x06004190 RID: 16784 RVA: 0x000C7C32 File Offset: 0x000C5E32
		public virtual bool HasValue
		{
			get
			{
				return this.Value != null;
			}
		}

		// Token: 0x1700256A RID: 9578
		// (get) Token: 0x06004191 RID: 16785 RVA: 0x000C7C40 File Offset: 0x000C5E40
		// (set) Token: 0x06004192 RID: 16786 RVA: 0x000C7C48 File Offset: 0x000C5E48
		public bool Optional { get; set; }

		// Token: 0x1700256B RID: 9579
		// (get) Token: 0x06004193 RID: 16787 RVA: 0x000C7C51 File Offset: 0x000C5E51
		// (set) Token: 0x06004194 RID: 16788 RVA: 0x000C7C59 File Offset: 0x000C5E59
		[TypeConverter(typeof(TypeNameConverter))]
		public Type TargetType { get; set; }

		// Token: 0x1700256C RID: 9580
		// (get) Token: 0x06004195 RID: 16789 RVA: 0x000C7C62 File Offset: 0x000C5E62
		// (set) Token: 0x06004196 RID: 16790 RVA: 0x000C7C6A File Offset: 0x000C5E6A
		public string DefaultValue { get; set; }

		// Token: 0x06004197 RID: 16791 RVA: 0x000C7C74 File Offset: 0x000C5E74
		public override string ToJavaScript(IControlResolver resolver)
		{
			object obj = this.Value ?? this.DefaultValue;
			if (this.TargetType != null)
			{
				obj = ValueConvertor.ConvertValue(obj, this.TargetType, null);
			}
			return string.Format("new StaticBinding({0})", obj.ToJsonString(null));
		}
	}
}
