using System;

namespace AjaxControlToolkit
{
	// Token: 0x02000005 RID: 5
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class ClientScriptResourceAttribute : Attribute
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002198 File Offset: 0x00000398
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000021A0 File Offset: 0x000003A0
		public string ComponentType
		{
			get
			{
				return this.componentType;
			}
			set
			{
				this.componentType = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021A9 File Offset: 0x000003A9
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000021B1 File Offset: 0x000003B1
		public int LoadOrder
		{
			get
			{
				return this.loadOrder;
			}
			set
			{
				this.loadOrder = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021BA File Offset: 0x000003BA
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000021C2 File Offset: 0x000003C2
		public string ResourcePath
		{
			get
			{
				return this.resourcePath;
			}
			set
			{
				this.resourcePath = value;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021CB File Offset: 0x000003CB
		public ClientScriptResourceAttribute()
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021D3 File Offset: 0x000003D3
		public ClientScriptResourceAttribute(string componentType)
		{
			this.componentType = componentType;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021E4 File Offset: 0x000003E4
		public ClientScriptResourceAttribute(string componentType, Type baseType, string resourceName)
		{
			if (baseType == null)
			{
				throw new ArgumentNullException("baseType");
			}
			if (resourceName == null)
			{
				throw new ArgumentNullException("resourceName");
			}
			string text = baseType.FullName;
			int num = text.LastIndexOf('.');
			if (num != -1)
			{
				text = text.Substring(0, num);
			}
			this.ResourcePath = text + "." + resourceName;
			this.componentType = componentType;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000224F File Offset: 0x0000044F
		public ClientScriptResourceAttribute(string componentType, string fullResourceName) : this(componentType)
		{
			if (fullResourceName == null)
			{
				throw new ArgumentNullException("fullResourceName");
			}
			this.ResourcePath = fullResourceName;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000226D File Offset: 0x0000046D
		public override bool IsDefaultAttribute()
		{
			return this.ComponentType == null && this.ResourcePath == null;
		}

		// Token: 0x04000004 RID: 4
		private string resourcePath;

		// Token: 0x04000005 RID: 5
		private string componentType;

		// Token: 0x04000006 RID: 6
		private int loadOrder;
	}
}
