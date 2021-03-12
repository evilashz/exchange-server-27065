using System;
using System.Runtime.InteropServices;

namespace System.Resources
{
	// Token: 0x02000366 RID: 870
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class NeutralResourcesLanguageAttribute : Attribute
	{
		// Token: 0x06002BEB RID: 11243 RVA: 0x000A578E File Offset: 0x000A398E
		[__DynamicallyInvokable]
		public NeutralResourcesLanguageAttribute(string cultureName)
		{
			if (cultureName == null)
			{
				throw new ArgumentNullException("cultureName");
			}
			this._culture = cultureName;
			this._fallbackLoc = UltimateResourceFallbackLocation.MainAssembly;
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x000A57B4 File Offset: 0x000A39B4
		public NeutralResourcesLanguageAttribute(string cultureName, UltimateResourceFallbackLocation location)
		{
			if (cultureName == null)
			{
				throw new ArgumentNullException("cultureName");
			}
			if (!Enum.IsDefined(typeof(UltimateResourceFallbackLocation), location))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidNeutralResourcesLanguage_FallbackLoc", new object[]
				{
					location
				}));
			}
			this._culture = cultureName;
			this._fallbackLoc = location;
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06002BED RID: 11245 RVA: 0x000A5819 File Offset: 0x000A3A19
		[__DynamicallyInvokable]
		public string CultureName
		{
			[__DynamicallyInvokable]
			get
			{
				return this._culture;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06002BEE RID: 11246 RVA: 0x000A5821 File Offset: 0x000A3A21
		public UltimateResourceFallbackLocation Location
		{
			get
			{
				return this._fallbackLoc;
			}
		}

		// Token: 0x04001173 RID: 4467
		private string _culture;

		// Token: 0x04001174 RID: 4468
		private UltimateResourceFallbackLocation _fallbackLoc;
	}
}
