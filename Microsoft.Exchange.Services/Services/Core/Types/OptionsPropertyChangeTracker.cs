using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000A67 RID: 2663
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class OptionsPropertyChangeTracker
	{
		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x06004B86 RID: 19334 RVA: 0x0010594C File Offset: 0x00103B4C
		private HashSet<string> PropertyNamesThatHaveChanged
		{
			get
			{
				if (this.propertyNamesThatHaveChanged == null)
				{
					this.propertyNamesThatHaveChanged = new HashSet<string>();
				}
				return this.propertyNamesThatHaveChanged;
			}
		}

		// Token: 0x06004B87 RID: 19335 RVA: 0x00105967 File Offset: 0x00103B67
		protected void TrackPropertyChanged([CallerMemberName] string propertyNameThatChanged = null)
		{
			if (!string.IsNullOrWhiteSpace(propertyNameThatChanged))
			{
				this.PropertyNamesThatHaveChanged.Add(propertyNameThatChanged);
			}
		}

		// Token: 0x06004B88 RID: 19336 RVA: 0x0010597E File Offset: 0x00103B7E
		public bool HasPropertyChanged(string propertyName)
		{
			return this.propertyNamesThatHaveChanged != null && this.propertyNamesThatHaveChanged.Contains(propertyName);
		}

		// Token: 0x06004B89 RID: 19337 RVA: 0x001059B8 File Offset: 0x00103BB8
		public override string ToString()
		{
			IEnumerable<string> values = from p in base.GetType().GetProperties()
			select p.Name + " = " + OptionsPropertyChangeTracker.GetStringValue(p.GetValue(this, null));
			return string.Join(",  ", values);
		}

		// Token: 0x06004B8A RID: 19338 RVA: 0x001059F8 File Offset: 0x00103BF8
		private static string GetStringValue(object value)
		{
			if (value == null)
			{
				return "<null>";
			}
			IEnumerable enumerable = value as IEnumerable;
			if (!(value is string) && enumerable != null)
			{
				IEnumerable<string> values = from object e in enumerable
				select OptionsPropertyChangeTracker.GetStringValue(e);
				return "{" + string.Join(",", values) + "}";
			}
			return value.ToString();
		}

		// Token: 0x04002AEE RID: 10990
		private HashSet<string> propertyNamesThatHaveChanged;
	}
}
