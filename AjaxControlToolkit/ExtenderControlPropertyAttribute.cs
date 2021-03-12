using System;

namespace AjaxControlToolkit
{
	// Token: 0x02000013 RID: 19
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class ExtenderControlPropertyAttribute : Attribute
	{
		// Token: 0x06000086 RID: 134 RVA: 0x0000314E File Offset: 0x0000134E
		public ExtenderControlPropertyAttribute() : this(true)
		{
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003157 File Offset: 0x00001357
		public ExtenderControlPropertyAttribute(bool isScriptProperty)
		{
			this.isScriptProperty = isScriptProperty;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003166 File Offset: 0x00001366
		public bool IsScriptProperty
		{
			get
			{
				return this.isScriptProperty;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003170 File Offset: 0x00001370
		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(obj, this))
			{
				return true;
			}
			ExtenderControlPropertyAttribute extenderControlPropertyAttribute = obj as ExtenderControlPropertyAttribute;
			return extenderControlPropertyAttribute != null && extenderControlPropertyAttribute.isScriptProperty == this.isScriptProperty;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000031A2 File Offset: 0x000013A2
		public override int GetHashCode()
		{
			return this.isScriptProperty.GetHashCode();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000031AF File Offset: 0x000013AF
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ExtenderControlPropertyAttribute.defaultValue);
		}

		// Token: 0x04000021 RID: 33
		private static ExtenderControlPropertyAttribute yes = new ExtenderControlPropertyAttribute(true);

		// Token: 0x04000022 RID: 34
		private static ExtenderControlPropertyAttribute no = new ExtenderControlPropertyAttribute(false);

		// Token: 0x04000023 RID: 35
		private static ExtenderControlPropertyAttribute defaultValue = ExtenderControlPropertyAttribute.no;

		// Token: 0x04000024 RID: 36
		private bool isScriptProperty;
	}
}
