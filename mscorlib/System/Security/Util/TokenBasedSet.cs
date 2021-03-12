using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Security.Util
{
	// Token: 0x02000351 RID: 849
	[Serializable]
	internal class TokenBasedSet
	{
		// Token: 0x06002B41 RID: 11073 RVA: 0x000A0E3C File Offset: 0x0009F03C
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this.OnDeserializedInternal();
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x000A0E44 File Offset: 0x0009F044
		private void OnDeserializedInternal()
		{
			if (this.m_objSet != null)
			{
				if (this.m_cElt == 1)
				{
					this.m_Obj = this.m_objSet[this.m_maxIndex];
				}
				else
				{
					this.m_Set = this.m_objSet;
				}
				this.m_objSet = null;
			}
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x000A0E90 File Offset: 0x0009F090
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				if (this.m_cElt == 1)
				{
					this.m_objSet = new object[this.m_maxIndex + 1];
					this.m_objSet[this.m_maxIndex] = this.m_Obj;
					return;
				}
				if (this.m_cElt > 0)
				{
					this.m_objSet = this.m_Set;
				}
			}
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x000A0EF9 File Offset: 0x0009F0F9
		[OnSerialized]
		private void OnSerialized(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				this.m_objSet = null;
			}
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x000A0F14 File Offset: 0x0009F114
		internal bool MoveNext(ref TokenBasedSetEnumerator e)
		{
			int num = this.m_cElt;
			if (num == 0)
			{
				return false;
			}
			if (num != 1)
			{
				do
				{
					num = e.Index + 1;
					e.Index = num;
					if (num > this.m_maxIndex)
					{
						goto Block_5;
					}
					e.Current = Volatile.Read<object>(ref this.m_Set[e.Index]);
				}
				while (e.Current == null);
				return true;
				Block_5:
				e.Current = null;
				return false;
			}
			if (e.Index == -1)
			{
				e.Index = this.m_maxIndex;
				e.Current = this.m_Obj;
				return true;
			}
			e.Index = (int)((short)(this.m_maxIndex + 1));
			e.Current = null;
			return false;
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x000A0FBC File Offset: 0x0009F1BC
		internal TokenBasedSet()
		{
			this.Reset();
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x000A0FDC File Offset: 0x0009F1DC
		internal TokenBasedSet(TokenBasedSet tbSet)
		{
			if (tbSet == null)
			{
				this.Reset();
				return;
			}
			if (tbSet.m_cElt > 1)
			{
				object[] set = tbSet.m_Set;
				int num = set.Length;
				object[] array = new object[num];
				Array.Copy(set, 0, array, 0, num);
				this.m_Set = array;
			}
			else
			{
				this.m_Obj = tbSet.m_Obj;
			}
			this.m_cElt = tbSet.m_cElt;
			this.m_maxIndex = tbSet.m_maxIndex;
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x000A1066 File Offset: 0x0009F266
		internal void Reset()
		{
			this.m_Obj = null;
			this.m_Set = null;
			this.m_cElt = 0;
			this.m_maxIndex = -1;
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x000A108C File Offset: 0x0009F28C
		internal void SetItem(int index, object item)
		{
			if (item == null)
			{
				this.RemoveItem(index);
				return;
			}
			int cElt = this.m_cElt;
			if (cElt == 0)
			{
				this.m_cElt = 1;
				this.m_maxIndex = (int)((short)index);
				this.m_Obj = item;
				return;
			}
			if (cElt != 1)
			{
				object[] array = this.m_Set;
				if (index >= array.Length)
				{
					object[] array2 = new object[index + 1];
					Array.Copy(array, 0, array2, 0, this.m_maxIndex + 1);
					this.m_maxIndex = (int)((short)index);
					array2[index] = item;
					this.m_Set = array2;
					this.m_cElt++;
					return;
				}
				if (array[index] == null)
				{
					this.m_cElt++;
				}
				array[index] = item;
				if (index > this.m_maxIndex)
				{
					this.m_maxIndex = (int)((short)index);
				}
				return;
			}
			else
			{
				if (index == this.m_maxIndex)
				{
					this.m_Obj = item;
					return;
				}
				object obj = this.m_Obj;
				int num = Math.Max(this.m_maxIndex, index);
				object[] array = new object[num + 1];
				array[this.m_maxIndex] = obj;
				array[index] = item;
				this.m_maxIndex = (int)((short)num);
				this.m_cElt = 2;
				this.m_Set = array;
				this.m_Obj = null;
				return;
			}
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x000A11C0 File Offset: 0x0009F3C0
		internal object GetItem(int index)
		{
			int cElt = this.m_cElt;
			if (cElt == 0)
			{
				return null;
			}
			if (cElt != 1)
			{
				if (index < this.m_Set.Length)
				{
					return Volatile.Read<object>(ref this.m_Set[index]);
				}
				return null;
			}
			else
			{
				if (index == this.m_maxIndex)
				{
					return this.m_Obj;
				}
				return null;
			}
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x000A1218 File Offset: 0x0009F418
		internal object RemoveItem(int index)
		{
			object result = null;
			int cElt = this.m_cElt;
			if (cElt != 0)
			{
				if (cElt != 1)
				{
					if (index < this.m_Set.Length && (result = Volatile.Read<object>(ref this.m_Set[index])) != null)
					{
						Volatile.Write<object>(ref this.m_Set[index], null);
						this.m_cElt--;
						if (index == this.m_maxIndex)
						{
							this.ResetMaxIndex(this.m_Set);
						}
						if (this.m_cElt == 1)
						{
							this.m_Obj = Volatile.Read<object>(ref this.m_Set[this.m_maxIndex]);
							this.m_Set = null;
						}
					}
				}
				else if (index != this.m_maxIndex)
				{
					result = null;
				}
				else
				{
					result = this.m_Obj;
					this.Reset();
				}
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x000A12FC File Offset: 0x0009F4FC
		private void ResetMaxIndex(object[] aObj)
		{
			for (int i = aObj.Length - 1; i >= 0; i--)
			{
				if (aObj[i] != null)
				{
					this.m_maxIndex = (int)((short)i);
					return;
				}
			}
			this.m_maxIndex = -1;
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x000A1332 File Offset: 0x0009F532
		internal int GetStartingIndex()
		{
			if (this.m_cElt <= 1)
			{
				return this.m_maxIndex;
			}
			return 0;
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x000A1347 File Offset: 0x0009F547
		internal int GetCount()
		{
			return this.m_cElt;
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x000A134F File Offset: 0x0009F54F
		internal int GetMaxUsedIndex()
		{
			return this.m_maxIndex;
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x000A1359 File Offset: 0x0009F559
		internal bool FastIsEmpty()
		{
			return this.m_cElt == 0;
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x000A1364 File Offset: 0x0009F564
		internal TokenBasedSet SpecialUnion(TokenBasedSet other)
		{
			this.OnDeserializedInternal();
			TokenBasedSet tokenBasedSet = new TokenBasedSet();
			int num;
			if (other != null)
			{
				other.OnDeserializedInternal();
				num = ((this.GetMaxUsedIndex() > other.GetMaxUsedIndex()) ? this.GetMaxUsedIndex() : other.GetMaxUsedIndex());
			}
			else
			{
				num = this.GetMaxUsedIndex();
			}
			for (int i = 0; i <= num; i++)
			{
				object item = this.GetItem(i);
				IPermission permission = item as IPermission;
				ISecurityElementFactory securityElementFactory = item as ISecurityElementFactory;
				object obj = (other != null) ? other.GetItem(i) : null;
				IPermission permission2 = obj as IPermission;
				ISecurityElementFactory securityElementFactory2 = obj as ISecurityElementFactory;
				if (item != null || obj != null)
				{
					if (item == null)
					{
						if (securityElementFactory2 != null)
						{
							permission2 = PermissionSet.CreatePerm(securityElementFactory2, false);
						}
						PermissionToken token = PermissionToken.GetToken(permission2);
						if (token == null)
						{
							throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
						}
						tokenBasedSet.SetItem(token.m_index, permission2);
					}
					else if (obj == null)
					{
						if (securityElementFactory != null)
						{
							permission = PermissionSet.CreatePerm(securityElementFactory, false);
						}
						PermissionToken token2 = PermissionToken.GetToken(permission);
						if (token2 == null)
						{
							throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
						}
						tokenBasedSet.SetItem(token2.m_index, permission);
					}
				}
			}
			return tokenBasedSet;
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x000A147C File Offset: 0x0009F67C
		internal void SpecialSplit(ref TokenBasedSet unrestrictedPermSet, ref TokenBasedSet normalPermSet, bool ignoreTypeLoadFailures)
		{
			int maxUsedIndex = this.GetMaxUsedIndex();
			for (int i = this.GetStartingIndex(); i <= maxUsedIndex; i++)
			{
				object item = this.GetItem(i);
				if (item != null)
				{
					IPermission permission = item as IPermission;
					if (permission == null)
					{
						permission = PermissionSet.CreatePerm(item, ignoreTypeLoadFailures);
					}
					PermissionToken token = PermissionToken.GetToken(permission);
					if (permission != null && token != null)
					{
						if (permission is IUnrestrictedPermission)
						{
							if (unrestrictedPermSet == null)
							{
								unrestrictedPermSet = new TokenBasedSet();
							}
							unrestrictedPermSet.SetItem(token.m_index, permission);
						}
						else
						{
							if (normalPermSet == null)
							{
								normalPermSet = new TokenBasedSet();
							}
							normalPermSet.SetItem(token.m_index, permission);
						}
					}
				}
			}
		}

		// Token: 0x04001116 RID: 4374
		private int m_initSize = 24;

		// Token: 0x04001117 RID: 4375
		private int m_increment = 8;

		// Token: 0x04001118 RID: 4376
		private object[] m_objSet;

		// Token: 0x04001119 RID: 4377
		[OptionalField(VersionAdded = 2)]
		private volatile object m_Obj;

		// Token: 0x0400111A RID: 4378
		[OptionalField(VersionAdded = 2)]
		private volatile object[] m_Set;

		// Token: 0x0400111B RID: 4379
		private int m_cElt;

		// Token: 0x0400111C RID: 4380
		private volatile int m_maxIndex;
	}
}
