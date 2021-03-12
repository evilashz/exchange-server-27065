using System;

namespace AjaxControlToolkit
{
	// Token: 0x02000003 RID: 3
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class ClientCssResourceAttribute : Attribute
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public ClientCssResourceAttribute(Type baseType, string resourceName)
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
			this.resourcePath = text + '.' + resourceName;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002136 File Offset: 0x00000336
		public ClientCssResourceAttribute(string fullResourceName)
		{
			if (fullResourceName == null)
			{
				throw new ArgumentNullException("fullResourceName");
			}
			this.resourcePath = fullResourceName;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002153 File Offset: 0x00000353
		public string ResourcePath
		{
			get
			{
				return this.resourcePath;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000215B File Offset: 0x0000035B
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002163 File Offset: 0x00000363
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

		// Token: 0x04000001 RID: 1
		private string resourcePath;

		// Token: 0x04000002 RID: 2
		private int loadOrder;
	}
}
