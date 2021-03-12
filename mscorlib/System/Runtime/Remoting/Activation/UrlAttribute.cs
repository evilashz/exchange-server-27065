using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000872 RID: 2162
	[SecurityCritical]
	[ComVisible(true)]
	[Serializable]
	public sealed class UrlAttribute : ContextAttribute
	{
		// Token: 0x06005C49 RID: 23625 RVA: 0x0014444C File Offset: 0x0014264C
		[SecurityCritical]
		public UrlAttribute(string callsiteURL) : base(UrlAttribute.propertyName)
		{
			if (callsiteURL == null)
			{
				throw new ArgumentNullException("callsiteURL");
			}
			this.url = callsiteURL;
		}

		// Token: 0x06005C4A RID: 23626 RVA: 0x0014446E File Offset: 0x0014266E
		[SecuritySafeCritical]
		public override bool Equals(object o)
		{
			return o is IContextProperty && o is UrlAttribute && ((UrlAttribute)o).UrlValue.Equals(this.url);
		}

		// Token: 0x06005C4B RID: 23627 RVA: 0x00144498 File Offset: 0x00142698
		[SecuritySafeCritical]
		public override int GetHashCode()
		{
			return this.url.GetHashCode();
		}

		// Token: 0x06005C4C RID: 23628 RVA: 0x001444A5 File Offset: 0x001426A5
		[SecurityCritical]
		[ComVisible(true)]
		public override bool IsContextOK(Context ctx, IConstructionCallMessage msg)
		{
			return false;
		}

		// Token: 0x06005C4D RID: 23629 RVA: 0x001444A8 File Offset: 0x001426A8
		[SecurityCritical]
		[ComVisible(true)]
		public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
		}

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x06005C4E RID: 23630 RVA: 0x001444AA File Offset: 0x001426AA
		public string UrlValue
		{
			[SecurityCritical]
			get
			{
				return this.url;
			}
		}

		// Token: 0x0400294F RID: 10575
		private string url;

		// Token: 0x04002950 RID: 10576
		private static string propertyName = "UrlAttribute";
	}
}
