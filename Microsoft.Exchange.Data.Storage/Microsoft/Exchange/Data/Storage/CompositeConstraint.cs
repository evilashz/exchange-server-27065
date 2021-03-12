using System;
using System.Text;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E9F RID: 3743
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class CompositeConstraint : StoreObjectConstraint
	{
		// Token: 0x0600820A RID: 33290 RVA: 0x002389EC File Offset: 0x00236BEC
		internal CompositeConstraint(StoreObjectConstraint[] constraints) : base(CompositeConstraint.GetPropertyDefinitions(constraints))
		{
			this.constraints = constraints;
		}

		// Token: 0x0600820B RID: 33291 RVA: 0x00238A04 File Offset: 0x00236C04
		private static PropertyDefinition[] GetPropertyDefinitions(StoreObjectConstraint[] constraints)
		{
			HashSet<PropertyDefinition> hashSet = new HashSet<PropertyDefinition>();
			foreach (StoreObjectConstraint storeObjectConstraint in constraints)
			{
				foreach (PropertyDefinition item in storeObjectConstraint.RelevantProperties)
				{
					hashSet.TryAdd(item);
				}
			}
			return hashSet.ToArray();
		}

		// Token: 0x1700227B RID: 8827
		// (get) Token: 0x0600820C RID: 33292 RVA: 0x00238A7C File Offset: 0x00236C7C
		protected StoreObjectConstraint[] Constraints
		{
			get
			{
				return this.constraints;
			}
		}

		// Token: 0x1700227C RID: 8828
		// (get) Token: 0x0600820D RID: 33293
		protected abstract string CompositionTypeDescription { get; }

		// Token: 0x0600820E RID: 33294 RVA: 0x00238A84 File Offset: 0x00236C84
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0}(", this.CompositionTypeDescription);
			foreach (StoreObjectConstraint storeObjectConstraint in this.constraints)
			{
				stringBuilder.AppendFormat("({0})", storeObjectConstraint.ToString());
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x04005758 RID: 22360
		private readonly StoreObjectConstraint[] constraints;
	}
}
