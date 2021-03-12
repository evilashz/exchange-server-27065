using System;

namespace AjaxControlToolkit
{
	// Token: 0x02000011 RID: 17
	[AttributeUsage(AttributeTargets.Property, Inherited = true)]
	public sealed class ExtenderControlEventAttribute : Attribute
	{
		// Token: 0x06000078 RID: 120 RVA: 0x0000302E File Offset: 0x0000122E
		public ExtenderControlEventAttribute() : this(true)
		{
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003037 File Offset: 0x00001237
		public ExtenderControlEventAttribute(bool isScriptEvent)
		{
			this.isScriptEvent = isScriptEvent;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003046 File Offset: 0x00001246
		public bool IsScriptEvent
		{
			get
			{
				return this.isScriptEvent;
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003050 File Offset: 0x00001250
		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(obj, this))
			{
				return true;
			}
			ExtenderControlEventAttribute extenderControlEventAttribute = obj as ExtenderControlEventAttribute;
			return extenderControlEventAttribute != null && extenderControlEventAttribute.isScriptEvent == this.isScriptEvent;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003082 File Offset: 0x00001282
		public override int GetHashCode()
		{
			return this.isScriptEvent.GetHashCode();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000308F File Offset: 0x0000128F
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ExtenderControlEventAttribute.defaultValue);
		}

		// Token: 0x04000019 RID: 25
		private static ExtenderControlEventAttribute yes = new ExtenderControlEventAttribute(true);

		// Token: 0x0400001A RID: 26
		private static ExtenderControlEventAttribute no = new ExtenderControlEventAttribute(false);

		// Token: 0x0400001B RID: 27
		private static ExtenderControlEventAttribute defaultValue = ExtenderControlEventAttribute.no;

		// Token: 0x0400001C RID: 28
		private bool isScriptEvent;
	}
}
