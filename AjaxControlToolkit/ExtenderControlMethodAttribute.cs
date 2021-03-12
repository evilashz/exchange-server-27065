using System;

namespace AjaxControlToolkit
{
	// Token: 0x02000012 RID: 18
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class ExtenderControlMethodAttribute : Attribute
	{
		// Token: 0x0600007F RID: 127 RVA: 0x000030BE File Offset: 0x000012BE
		public ExtenderControlMethodAttribute() : this(true)
		{
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000030C7 File Offset: 0x000012C7
		public ExtenderControlMethodAttribute(bool isScriptMethod)
		{
			this.isScriptMethod = isScriptMethod;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000030D6 File Offset: 0x000012D6
		public bool IsScriptMethod
		{
			get
			{
				return this.isScriptMethod;
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000030E0 File Offset: 0x000012E0
		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(obj, this))
			{
				return true;
			}
			ExtenderControlMethodAttribute extenderControlMethodAttribute = obj as ExtenderControlMethodAttribute;
			return extenderControlMethodAttribute != null && extenderControlMethodAttribute.isScriptMethod == this.isScriptMethod;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003112 File Offset: 0x00001312
		public override int GetHashCode()
		{
			return this.isScriptMethod.GetHashCode();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000311F File Offset: 0x0000131F
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ExtenderControlMethodAttribute.defaultValue);
		}

		// Token: 0x0400001D RID: 29
		private static ExtenderControlMethodAttribute yes = new ExtenderControlMethodAttribute(true);

		// Token: 0x0400001E RID: 30
		private static ExtenderControlMethodAttribute no = new ExtenderControlMethodAttribute(false);

		// Token: 0x0400001F RID: 31
		private static ExtenderControlMethodAttribute defaultValue = ExtenderControlMethodAttribute.no;

		// Token: 0x04000020 RID: 32
		private bool isScriptMethod;
	}
}
