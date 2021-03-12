using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Resources
{
	// Token: 0x02000365 RID: 869
	[ComVisible(true)]
	[Serializable]
	public class MissingSatelliteAssemblyException : SystemException
	{
		// Token: 0x06002BE5 RID: 11237 RVA: 0x000A571B File Offset: 0x000A391B
		public MissingSatelliteAssemblyException() : base(Environment.GetResourceString("MissingSatelliteAssembly_Default"))
		{
			base.SetErrorCode(-2146233034);
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x000A5738 File Offset: 0x000A3938
		public MissingSatelliteAssemblyException(string message) : base(message)
		{
			base.SetErrorCode(-2146233034);
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x000A574C File Offset: 0x000A394C
		public MissingSatelliteAssemblyException(string message, string cultureName) : base(message)
		{
			base.SetErrorCode(-2146233034);
			this._cultureName = cultureName;
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x000A5767 File Offset: 0x000A3967
		public MissingSatelliteAssemblyException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233034);
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x000A577C File Offset: 0x000A397C
		protected MissingSatelliteAssemblyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06002BEA RID: 11242 RVA: 0x000A5786 File Offset: 0x000A3986
		public string CultureName
		{
			get
			{
				return this._cultureName;
			}
		}

		// Token: 0x04001172 RID: 4466
		private string _cultureName;
	}
}
