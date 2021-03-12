using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000217 RID: 535
	public sealed class PersonaComparerByDisplayName : IComparer<Persona>
	{
		// Token: 0x060014A4 RID: 5284 RVA: 0x00049372 File Offset: 0x00047572
		public PersonaComparerByDisplayName(CultureInfo culture)
		{
			this.culture = culture;
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x00049381 File Offset: 0x00047581
		public int Compare(Persona persona1, Persona persona2)
		{
			return string.Compare(persona1.DisplayName, persona2.DisplayName, this.culture, CompareOptions.None);
		}

		// Token: 0x04000B34 RID: 2868
		private readonly CultureInfo culture;
	}
}
