using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Principal
{
	// Token: 0x02000308 RID: 776
	[ComVisible(false)]
	public class IdentityReferenceCollection : ICollection<IdentityReference>, IEnumerable<IdentityReference>, IEnumerable
	{
		// Token: 0x060027DE RID: 10206 RVA: 0x00092638 File Offset: 0x00090838
		public IdentityReferenceCollection() : this(0)
		{
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x00092641 File Offset: 0x00090841
		public IdentityReferenceCollection(int capacity)
		{
			this._Identities = new List<IdentityReference>(capacity);
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x00092655 File Offset: 0x00090855
		public void CopyTo(IdentityReference[] array, int offset)
		{
			this._Identities.CopyTo(0, array, offset, this.Count);
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060027E1 RID: 10209 RVA: 0x0009266B File Offset: 0x0009086B
		public int Count
		{
			get
			{
				return this._Identities.Count;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060027E2 RID: 10210 RVA: 0x00092678 File Offset: 0x00090878
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x0009267B File Offset: 0x0009087B
		public void Add(IdentityReference identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this._Identities.Add(identity);
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x0009269D File Offset: 0x0009089D
		public bool Remove(IdentityReference identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (this.Contains(identity))
			{
				this._Identities.Remove(identity);
				return true;
			}
			return false;
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x000926CC File Offset: 0x000908CC
		public void Clear()
		{
			this._Identities.Clear();
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x000926D9 File Offset: 0x000908D9
		public bool Contains(IdentityReference identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			return this._Identities.Contains(identity);
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x000926FB File Offset: 0x000908FB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x00092703 File Offset: 0x00090903
		public IEnumerator<IdentityReference> GetEnumerator()
		{
			return new IdentityReferenceEnumerator(this);
		}

		// Token: 0x17000541 RID: 1345
		public IdentityReference this[int index]
		{
			get
			{
				return this._Identities[index];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._Identities[index] = value;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060027EB RID: 10219 RVA: 0x0009273C File Offset: 0x0009093C
		internal List<IdentityReference> Identities
		{
			get
			{
				return this._Identities;
			}
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x00092744 File Offset: 0x00090944
		public IdentityReferenceCollection Translate(Type targetType)
		{
			return this.Translate(targetType, false);
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x00092750 File Offset: 0x00090950
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public IdentityReferenceCollection Translate(Type targetType, bool forceSuccess)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			if (!targetType.IsSubclassOf(typeof(IdentityReference)))
			{
				throw new ArgumentException(Environment.GetResourceString("IdentityReference_MustBeIdentityReference"), "targetType");
			}
			if (this.Identities.Count == 0)
			{
				return new IdentityReferenceCollection();
			}
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < this.Identities.Count; i++)
			{
				Type type = this.Identities[i].GetType();
				if (!(type == targetType))
				{
					if (type == typeof(SecurityIdentifier))
					{
						num++;
					}
					else
					{
						if (!(type == typeof(NTAccount)))
						{
							throw new SystemException();
						}
						num2++;
					}
				}
			}
			bool flag = false;
			IdentityReferenceCollection identityReferenceCollection = null;
			IdentityReferenceCollection identityReferenceCollection2 = null;
			if (num == this.Count)
			{
				flag = true;
				identityReferenceCollection = this;
			}
			else if (num > 0)
			{
				identityReferenceCollection = new IdentityReferenceCollection(num);
			}
			if (num2 == this.Count)
			{
				flag = true;
				identityReferenceCollection2 = this;
			}
			else if (num2 > 0)
			{
				identityReferenceCollection2 = new IdentityReferenceCollection(num2);
			}
			IdentityReferenceCollection identityReferenceCollection3 = null;
			if (!flag)
			{
				identityReferenceCollection3 = new IdentityReferenceCollection(this.Identities.Count);
				for (int j = 0; j < this.Identities.Count; j++)
				{
					IdentityReference identityReference = this[j];
					Type type2 = identityReference.GetType();
					if (!(type2 == targetType))
					{
						if (type2 == typeof(SecurityIdentifier))
						{
							identityReferenceCollection.Add(identityReference);
						}
						else
						{
							if (!(type2 == typeof(NTAccount)))
							{
								throw new SystemException();
							}
							identityReferenceCollection2.Add(identityReference);
						}
					}
				}
			}
			bool flag2 = false;
			IdentityReferenceCollection identityReferenceCollection4 = null;
			IdentityReferenceCollection identityReferenceCollection5 = null;
			if (num > 0)
			{
				identityReferenceCollection4 = SecurityIdentifier.Translate(identityReferenceCollection, targetType, out flag2);
				if (flag && (!forceSuccess || !flag2))
				{
					identityReferenceCollection3 = identityReferenceCollection4;
				}
			}
			if (num2 > 0)
			{
				identityReferenceCollection5 = NTAccount.Translate(identityReferenceCollection2, targetType, out flag2);
				if (flag && (!forceSuccess || !flag2))
				{
					identityReferenceCollection3 = identityReferenceCollection5;
				}
			}
			if (forceSuccess && flag2)
			{
				identityReferenceCollection3 = new IdentityReferenceCollection();
				if (identityReferenceCollection4 != null)
				{
					foreach (IdentityReference identityReference2 in identityReferenceCollection4)
					{
						if (identityReference2.GetType() != targetType)
						{
							identityReferenceCollection3.Add(identityReference2);
						}
					}
				}
				if (identityReferenceCollection5 != null)
				{
					foreach (IdentityReference identityReference3 in identityReferenceCollection5)
					{
						if (identityReference3.GetType() != targetType)
						{
							identityReferenceCollection3.Add(identityReference3);
						}
					}
				}
				throw new IdentityNotMappedException(Environment.GetResourceString("IdentityReference_IdentityNotMapped"), identityReferenceCollection3);
			}
			if (!flag)
			{
				num = 0;
				num2 = 0;
				identityReferenceCollection3 = new IdentityReferenceCollection(this.Identities.Count);
				for (int k = 0; k < this.Identities.Count; k++)
				{
					IdentityReference identityReference4 = this[k];
					Type type3 = identityReference4.GetType();
					if (type3 == targetType)
					{
						identityReferenceCollection3.Add(identityReference4);
					}
					else if (type3 == typeof(SecurityIdentifier))
					{
						identityReferenceCollection3.Add(identityReferenceCollection4[num++]);
					}
					else
					{
						if (!(type3 == typeof(NTAccount)))
						{
							throw new SystemException();
						}
						identityReferenceCollection3.Add(identityReferenceCollection5[num2++]);
					}
				}
			}
			return identityReferenceCollection3;
		}

		// Token: 0x04000FD5 RID: 4053
		private List<IdentityReference> _Identities;
	}
}
