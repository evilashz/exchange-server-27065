using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Resources
{
	// Token: 0x02000367 RID: 871
	internal class ResourceFallbackManager : IEnumerable<CultureInfo>, IEnumerable
	{
		// Token: 0x06002BEF RID: 11247 RVA: 0x000A5829 File Offset: 0x000A3A29
		internal ResourceFallbackManager(CultureInfo startingCulture, CultureInfo neutralResourcesCulture, bool useParents)
		{
			if (startingCulture != null)
			{
				this.m_startingCulture = startingCulture;
			}
			else
			{
				this.m_startingCulture = CultureInfo.CurrentUICulture;
			}
			this.m_neutralResourcesCulture = neutralResourcesCulture;
			this.m_useParents = useParents;
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x000A5856 File Offset: 0x000A3A56
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x000A585E File Offset: 0x000A3A5E
		public IEnumerator<CultureInfo> GetEnumerator()
		{
			bool reachedNeutralResourcesCulture = false;
			CultureInfo currentCulture = this.m_startingCulture;
			while (this.m_neutralResourcesCulture == null || !(currentCulture.Name == this.m_neutralResourcesCulture.Name))
			{
				yield return currentCulture;
				currentCulture = currentCulture.Parent;
				if (!this.m_useParents || currentCulture.HasInvariantCultureName)
				{
					IL_CE:
					if (!this.m_useParents || this.m_startingCulture.HasInvariantCultureName)
					{
						yield break;
					}
					if (reachedNeutralResourcesCulture)
					{
						yield break;
					}
					yield return CultureInfo.InvariantCulture;
					yield break;
				}
			}
			yield return CultureInfo.InvariantCulture;
			reachedNeutralResourcesCulture = true;
			goto IL_CE;
		}

		// Token: 0x04001175 RID: 4469
		private CultureInfo m_startingCulture;

		// Token: 0x04001176 RID: 4470
		private CultureInfo m_neutralResourcesCulture;

		// Token: 0x04001177 RID: 4471
		private bool m_useParents;
	}
}
