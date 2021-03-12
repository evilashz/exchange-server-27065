using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x0200071F RID: 1823
	internal sealed class ObjectHolder
	{
		// Token: 0x06005169 RID: 20841 RVA: 0x0011DB72 File Offset: 0x0011BD72
		internal ObjectHolder(long objID) : this(null, objID, null, null, 0L, null, null)
		{
		}

		// Token: 0x0600516A RID: 20842 RVA: 0x0011DB84 File Offset: 0x0011BD84
		internal ObjectHolder(object obj, long objID, SerializationInfo info, ISerializationSurrogate surrogate, long idOfContainingObj, FieldInfo field, int[] arrayIndex)
		{
			this.m_object = obj;
			this.m_id = objID;
			this.m_flags = 0;
			this.m_missingElementsRemaining = 0;
			this.m_missingDecendents = 0;
			this.m_dependentObjects = null;
			this.m_next = null;
			this.m_serInfo = info;
			this.m_surrogate = surrogate;
			this.m_markForFixupWhenAvailable = false;
			if (obj is TypeLoadExceptionHolder)
			{
				this.m_typeLoad = (TypeLoadExceptionHolder)obj;
			}
			if (idOfContainingObj != 0L && ((field != null && field.FieldType.IsValueType) || arrayIndex != null))
			{
				if (idOfContainingObj == objID)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_ParentChildIdentical"));
				}
				this.m_valueFixup = new ValueTypeFixupInfo(idOfContainingObj, field, arrayIndex);
			}
			this.SetFlags();
		}

		// Token: 0x0600516B RID: 20843 RVA: 0x0011DC40 File Offset: 0x0011BE40
		internal ObjectHolder(string obj, long objID, SerializationInfo info, ISerializationSurrogate surrogate, long idOfContainingObj, FieldInfo field, int[] arrayIndex)
		{
			this.m_object = obj;
			this.m_id = objID;
			this.m_flags = 0;
			this.m_missingElementsRemaining = 0;
			this.m_missingDecendents = 0;
			this.m_dependentObjects = null;
			this.m_next = null;
			this.m_serInfo = info;
			this.m_surrogate = surrogate;
			this.m_markForFixupWhenAvailable = false;
			if (idOfContainingObj != 0L && arrayIndex != null)
			{
				this.m_valueFixup = new ValueTypeFixupInfo(idOfContainingObj, field, arrayIndex);
			}
			if (this.m_valueFixup != null)
			{
				this.m_flags |= 8;
			}
		}

		// Token: 0x0600516C RID: 20844 RVA: 0x0011DCC9 File Offset: 0x0011BEC9
		private void IncrementDescendentFixups(int amount)
		{
			this.m_missingDecendents += amount;
		}

		// Token: 0x0600516D RID: 20845 RVA: 0x0011DCD9 File Offset: 0x0011BED9
		internal void DecrementFixupsRemaining(ObjectManager manager)
		{
			this.m_missingElementsRemaining--;
			if (this.RequiresValueTypeFixup)
			{
				this.UpdateDescendentDependencyChain(-1, manager);
			}
		}

		// Token: 0x0600516E RID: 20846 RVA: 0x0011DCF9 File Offset: 0x0011BEF9
		internal void RemoveDependency(long id)
		{
			this.m_dependentObjects.RemoveElement(id);
		}

		// Token: 0x0600516F RID: 20847 RVA: 0x0011DD08 File Offset: 0x0011BF08
		internal void AddFixup(FixupHolder fixup, ObjectManager manager)
		{
			if (this.m_missingElements == null)
			{
				this.m_missingElements = new FixupHolderList();
			}
			this.m_missingElements.Add(fixup);
			this.m_missingElementsRemaining++;
			if (this.RequiresValueTypeFixup)
			{
				this.UpdateDescendentDependencyChain(1, manager);
			}
		}

		// Token: 0x06005170 RID: 20848 RVA: 0x0011DD48 File Offset: 0x0011BF48
		private void UpdateDescendentDependencyChain(int amount, ObjectManager manager)
		{
			ObjectHolder objectHolder = this;
			do
			{
				objectHolder = manager.FindOrCreateObjectHolder(objectHolder.ContainerID);
				objectHolder.IncrementDescendentFixups(amount);
			}
			while (objectHolder.RequiresValueTypeFixup);
		}

		// Token: 0x06005171 RID: 20849 RVA: 0x0011DD73 File Offset: 0x0011BF73
		internal void AddDependency(long dependentObject)
		{
			if (this.m_dependentObjects == null)
			{
				this.m_dependentObjects = new LongList();
			}
			this.m_dependentObjects.Add(dependentObject);
		}

		// Token: 0x06005172 RID: 20850 RVA: 0x0011DD94 File Offset: 0x0011BF94
		[SecurityCritical]
		internal void UpdateData(object obj, SerializationInfo info, ISerializationSurrogate surrogate, long idOfContainer, FieldInfo field, int[] arrayIndex, ObjectManager manager)
		{
			this.SetObjectValue(obj, manager);
			this.m_serInfo = info;
			this.m_surrogate = surrogate;
			if (idOfContainer != 0L && ((field != null && field.FieldType.IsValueType) || arrayIndex != null))
			{
				if (idOfContainer == this.m_id)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_ParentChildIdentical"));
				}
				this.m_valueFixup = new ValueTypeFixupInfo(idOfContainer, field, arrayIndex);
			}
			this.SetFlags();
			if (this.RequiresValueTypeFixup)
			{
				this.UpdateDescendentDependencyChain(this.m_missingElementsRemaining, manager);
			}
		}

		// Token: 0x06005173 RID: 20851 RVA: 0x0011DE1F File Offset: 0x0011C01F
		internal void MarkForCompletionWhenAvailable()
		{
			this.m_markForFixupWhenAvailable = true;
		}

		// Token: 0x06005174 RID: 20852 RVA: 0x0011DE28 File Offset: 0x0011C028
		internal void SetFlags()
		{
			if (this.m_object is IObjectReference)
			{
				this.m_flags |= 1;
			}
			this.m_flags &= -7;
			if (this.m_surrogate != null)
			{
				this.m_flags |= 4;
			}
			else if (this.m_object is ISerializable)
			{
				this.m_flags |= 2;
			}
			if (this.m_valueFixup != null)
			{
				this.m_flags |= 8;
			}
		}

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x06005175 RID: 20853 RVA: 0x0011DEA8 File Offset: 0x0011C0A8
		// (set) Token: 0x06005176 RID: 20854 RVA: 0x0011DEB5 File Offset: 0x0011C0B5
		internal bool IsIncompleteObjectReference
		{
			get
			{
				return (this.m_flags & 1) != 0;
			}
			set
			{
				if (value)
				{
					this.m_flags |= 1;
					return;
				}
				this.m_flags &= -2;
			}
		}

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x06005177 RID: 20855 RVA: 0x0011DED8 File Offset: 0x0011C0D8
		internal bool RequiresDelayedFixup
		{
			get
			{
				return (this.m_flags & 7) != 0;
			}
		}

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x06005178 RID: 20856 RVA: 0x0011DEE5 File Offset: 0x0011C0E5
		internal bool RequiresValueTypeFixup
		{
			get
			{
				return (this.m_flags & 8) != 0;
			}
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x06005179 RID: 20857 RVA: 0x0011DEF2 File Offset: 0x0011C0F2
		// (set) Token: 0x0600517A RID: 20858 RVA: 0x0011DF26 File Offset: 0x0011C126
		internal bool ValueTypeFixupPerformed
		{
			get
			{
				return (this.m_flags & 32768) != 0 || (this.m_object != null && (this.m_dependentObjects == null || this.m_dependentObjects.Count == 0));
			}
			set
			{
				if (value)
				{
					this.m_flags |= 32768;
				}
			}
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x0600517B RID: 20859 RVA: 0x0011DF3D File Offset: 0x0011C13D
		internal bool HasISerializable
		{
			get
			{
				return (this.m_flags & 2) != 0;
			}
		}

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x0600517C RID: 20860 RVA: 0x0011DF4A File Offset: 0x0011C14A
		internal bool HasSurrogate
		{
			get
			{
				return (this.m_flags & 4) != 0;
			}
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x0600517D RID: 20861 RVA: 0x0011DF57 File Offset: 0x0011C157
		internal bool CanSurrogatedObjectValueChange
		{
			get
			{
				return this.m_surrogate == null || this.m_surrogate.GetType() != typeof(SurrogateForCyclicalReference);
			}
		}

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x0600517E RID: 20862 RVA: 0x0011DF7D File Offset: 0x0011C17D
		internal bool CanObjectValueChange
		{
			get
			{
				return this.IsIncompleteObjectReference || (this.HasSurrogate && this.CanSurrogatedObjectValueChange);
			}
		}

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x0600517F RID: 20863 RVA: 0x0011DF99 File Offset: 0x0011C199
		internal int DirectlyDependentObjects
		{
			get
			{
				return this.m_missingElementsRemaining;
			}
		}

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x06005180 RID: 20864 RVA: 0x0011DFA1 File Offset: 0x0011C1A1
		internal int TotalDependentObjects
		{
			get
			{
				return this.m_missingElementsRemaining + this.m_missingDecendents;
			}
		}

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x06005181 RID: 20865 RVA: 0x0011DFB0 File Offset: 0x0011C1B0
		// (set) Token: 0x06005182 RID: 20866 RVA: 0x0011DFB8 File Offset: 0x0011C1B8
		internal bool Reachable
		{
			get
			{
				return this.m_reachable;
			}
			set
			{
				this.m_reachable = value;
			}
		}

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x06005183 RID: 20867 RVA: 0x0011DFC1 File Offset: 0x0011C1C1
		internal bool TypeLoadExceptionReachable
		{
			get
			{
				return this.m_typeLoad != null;
			}
		}

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x06005184 RID: 20868 RVA: 0x0011DFCC File Offset: 0x0011C1CC
		// (set) Token: 0x06005185 RID: 20869 RVA: 0x0011DFD4 File Offset: 0x0011C1D4
		internal TypeLoadExceptionHolder TypeLoadException
		{
			get
			{
				return this.m_typeLoad;
			}
			set
			{
				this.m_typeLoad = value;
			}
		}

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x06005186 RID: 20870 RVA: 0x0011DFDD File Offset: 0x0011C1DD
		internal object ObjectValue
		{
			get
			{
				return this.m_object;
			}
		}

		// Token: 0x06005187 RID: 20871 RVA: 0x0011DFE5 File Offset: 0x0011C1E5
		[SecurityCritical]
		internal void SetObjectValue(object obj, ObjectManager manager)
		{
			this.m_object = obj;
			if (obj == manager.TopObject)
			{
				this.m_reachable = true;
			}
			if (obj is TypeLoadExceptionHolder)
			{
				this.m_typeLoad = (TypeLoadExceptionHolder)obj;
			}
			if (this.m_markForFixupWhenAvailable)
			{
				manager.CompleteObject(this, true);
			}
		}

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x06005188 RID: 20872 RVA: 0x0011E022 File Offset: 0x0011C222
		// (set) Token: 0x06005189 RID: 20873 RVA: 0x0011E02A File Offset: 0x0011C22A
		internal SerializationInfo SerializationInfo
		{
			get
			{
				return this.m_serInfo;
			}
			set
			{
				this.m_serInfo = value;
			}
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x0600518A RID: 20874 RVA: 0x0011E033 File Offset: 0x0011C233
		internal ISerializationSurrogate Surrogate
		{
			get
			{
				return this.m_surrogate;
			}
		}

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x0600518B RID: 20875 RVA: 0x0011E03B File Offset: 0x0011C23B
		// (set) Token: 0x0600518C RID: 20876 RVA: 0x0011E043 File Offset: 0x0011C243
		internal LongList DependentObjects
		{
			get
			{
				return this.m_dependentObjects;
			}
			set
			{
				this.m_dependentObjects = value;
			}
		}

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x0600518D RID: 20877 RVA: 0x0011E04C File Offset: 0x0011C24C
		// (set) Token: 0x0600518E RID: 20878 RVA: 0x0011E073 File Offset: 0x0011C273
		internal bool RequiresSerInfoFixup
		{
			get
			{
				return ((this.m_flags & 4) != 0 || (this.m_flags & 2) != 0) && (this.m_flags & 16384) == 0;
			}
			set
			{
				if (!value)
				{
					this.m_flags |= 16384;
					return;
				}
				this.m_flags &= -16385;
			}
		}

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x0600518F RID: 20879 RVA: 0x0011E09D File Offset: 0x0011C29D
		internal ValueTypeFixupInfo ValueFixup
		{
			get
			{
				return this.m_valueFixup;
			}
		}

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x06005190 RID: 20880 RVA: 0x0011E0A5 File Offset: 0x0011C2A5
		internal bool CompletelyFixed
		{
			get
			{
				return !this.RequiresSerInfoFixup && !this.IsIncompleteObjectReference;
			}
		}

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x06005191 RID: 20881 RVA: 0x0011E0BA File Offset: 0x0011C2BA
		internal long ContainerID
		{
			get
			{
				if (this.m_valueFixup != null)
				{
					return this.m_valueFixup.ContainerID;
				}
				return 0L;
			}
		}

		// Token: 0x040023CF RID: 9167
		internal const int INCOMPLETE_OBJECT_REFERENCE = 1;

		// Token: 0x040023D0 RID: 9168
		internal const int HAS_ISERIALIZABLE = 2;

		// Token: 0x040023D1 RID: 9169
		internal const int HAS_SURROGATE = 4;

		// Token: 0x040023D2 RID: 9170
		internal const int REQUIRES_VALUETYPE_FIXUP = 8;

		// Token: 0x040023D3 RID: 9171
		internal const int REQUIRES_DELAYED_FIXUP = 7;

		// Token: 0x040023D4 RID: 9172
		internal const int SER_INFO_FIXED = 16384;

		// Token: 0x040023D5 RID: 9173
		internal const int VALUETYPE_FIXUP_PERFORMED = 32768;

		// Token: 0x040023D6 RID: 9174
		private object m_object;

		// Token: 0x040023D7 RID: 9175
		internal long m_id;

		// Token: 0x040023D8 RID: 9176
		private int m_missingElementsRemaining;

		// Token: 0x040023D9 RID: 9177
		private int m_missingDecendents;

		// Token: 0x040023DA RID: 9178
		internal SerializationInfo m_serInfo;

		// Token: 0x040023DB RID: 9179
		internal ISerializationSurrogate m_surrogate;

		// Token: 0x040023DC RID: 9180
		internal FixupHolderList m_missingElements;

		// Token: 0x040023DD RID: 9181
		internal LongList m_dependentObjects;

		// Token: 0x040023DE RID: 9182
		internal ObjectHolder m_next;

		// Token: 0x040023DF RID: 9183
		internal int m_flags;

		// Token: 0x040023E0 RID: 9184
		private bool m_markForFixupWhenAvailable;

		// Token: 0x040023E1 RID: 9185
		private ValueTypeFixupInfo m_valueFixup;

		// Token: 0x040023E2 RID: 9186
		private TypeLoadExceptionHolder m_typeLoad;

		// Token: 0x040023E3 RID: 9187
		private bool m_reachable;
	}
}
