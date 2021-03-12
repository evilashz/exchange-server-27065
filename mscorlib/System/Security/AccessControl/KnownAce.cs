﻿using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000201 RID: 513
	public abstract class KnownAce : GenericAce
	{
		// Token: 0x06001E55 RID: 7765 RVA: 0x0006A048 File Offset: 0x00068248
		internal KnownAce(AceType type, AceFlags flags, int accessMask, SecurityIdentifier securityIdentifier) : base(type, flags)
		{
			if (securityIdentifier == null)
			{
				throw new ArgumentNullException("securityIdentifier");
			}
			this.AccessMask = accessMask;
			this.SecurityIdentifier = securityIdentifier;
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001E56 RID: 7766 RVA: 0x0006A076 File Offset: 0x00068276
		// (set) Token: 0x06001E57 RID: 7767 RVA: 0x0006A07E File Offset: 0x0006827E
		public int AccessMask
		{
			get
			{
				return this._accessMask;
			}
			set
			{
				this._accessMask = value;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001E58 RID: 7768 RVA: 0x0006A087 File Offset: 0x00068287
		// (set) Token: 0x06001E59 RID: 7769 RVA: 0x0006A08F File Offset: 0x0006828F
		public SecurityIdentifier SecurityIdentifier
		{
			get
			{
				return this._sid;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._sid = value;
			}
		}

		// Token: 0x04000AE8 RID: 2792
		private int _accessMask;

		// Token: 0x04000AE9 RID: 2793
		private SecurityIdentifier _sid;

		// Token: 0x04000AEA RID: 2794
		internal const int AccessMaskLength = 4;
	}
}
