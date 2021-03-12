using System;

namespace AjaxControlToolkit
{
	// Token: 0x02000018 RID: 24
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class RequiredScriptAttribute : Attribute
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000333D File Offset: 0x0000153D
		public Type ExtenderType
		{
			get
			{
				return this.extenderType;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003345 File Offset: 0x00001545
		public string ScriptName
		{
			get
			{
				return this.scriptName;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000334D File Offset: 0x0000154D
		public int LoadOrder
		{
			get
			{
				return this.order;
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003355 File Offset: 0x00001555
		public RequiredScriptAttribute()
		{
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000335D File Offset: 0x0000155D
		public RequiredScriptAttribute(string scriptName)
		{
			this.scriptName = scriptName;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000336C File Offset: 0x0000156C
		public RequiredScriptAttribute(Type extenderType) : this(extenderType, 0)
		{
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003376 File Offset: 0x00001576
		public RequiredScriptAttribute(Type extenderType, int loadOrder)
		{
			this.extenderType = extenderType;
			this.order = loadOrder;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000338C File Offset: 0x0000158C
		public override bool IsDefaultAttribute()
		{
			return this.extenderType == null;
		}

		// Token: 0x04000028 RID: 40
		private int order;

		// Token: 0x04000029 RID: 41
		private Type extenderType;

		// Token: 0x0400002A RID: 42
		private string scriptName;
	}
}
