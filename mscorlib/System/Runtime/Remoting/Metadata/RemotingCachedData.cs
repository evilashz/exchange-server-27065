using System;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020007A3 RID: 1955
	internal abstract class RemotingCachedData
	{
		// Token: 0x060055AB RID: 21931 RVA: 0x0012F87C File Offset: 0x0012DA7C
		internal SoapAttribute GetSoapAttribute()
		{
			if (this._soapAttr == null)
			{
				lock (this)
				{
					if (this._soapAttr == null)
					{
						this._soapAttr = this.GetSoapAttributeNoLock();
					}
				}
			}
			return this._soapAttr;
		}

		// Token: 0x060055AC RID: 21932
		internal abstract SoapAttribute GetSoapAttributeNoLock();

		// Token: 0x040026FA RID: 9978
		private SoapAttribute _soapAttr;
	}
}
