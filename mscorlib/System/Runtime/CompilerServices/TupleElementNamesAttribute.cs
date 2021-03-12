using System;
using System.Collections.Generic;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008D1 RID: 2257
	[CLSCompliant(false)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public sealed class TupleElementNamesAttribute : Attribute
	{
		// Token: 0x06005D38 RID: 23864 RVA: 0x00146A13 File Offset: 0x00144C13
		public TupleElementNamesAttribute(string[] transformNames)
		{
			if (transformNames == null)
			{
				throw new ArgumentNullException("transformNames");
			}
			this._transformNames = transformNames;
		}

		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x06005D39 RID: 23865 RVA: 0x00146A30 File Offset: 0x00144C30
		public IList<string> TransformNames
		{
			get
			{
				return this._transformNames;
			}
		}

		// Token: 0x0400299E RID: 10654
		private readonly string[] _transformNames;
	}
}
