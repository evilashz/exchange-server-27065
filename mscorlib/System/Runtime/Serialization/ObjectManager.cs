﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Principal;
using System.Text;

namespace System.Runtime.Serialization
{
	// Token: 0x0200071E RID: 1822
	[ComVisible(true)]
	public class ObjectManager
	{
		// Token: 0x06005144 RID: 20804 RVA: 0x0011CAAE File Offset: 0x0011ACAE
		[SecuritySafeCritical]
		public ObjectManager(ISurrogateSelector selector, StreamingContext context) : this(selector, context, true, false)
		{
		}

		// Token: 0x06005145 RID: 20805 RVA: 0x0011CABC File Offset: 0x0011ACBC
		[SecurityCritical]
		internal ObjectManager(ISurrogateSelector selector, StreamingContext context, bool checkSecurity, bool isCrossAppDomain)
		{
			if (checkSecurity)
			{
				CodeAccessPermission.Demand(PermissionType.SecuritySerialization);
			}
			this.m_objects = new ObjectHolder[16];
			this.m_selector = selector;
			this.m_context = context;
			this.m_isCrossAppDomain = isCrossAppDomain;
			if (!isCrossAppDomain && AppContextSwitches.UseNewMaxArraySize)
			{
				this.MaxArraySize = 16777216;
			}
			else
			{
				this.MaxArraySize = 4096;
			}
			this.ArrayMask = this.MaxArraySize - 1;
		}

		// Token: 0x06005146 RID: 20806 RVA: 0x0011CB2C File Offset: 0x0011AD2C
		[SecurityCritical]
		private bool CanCallGetType(object obj)
		{
			return !RemotingServices.IsTransparentProxy(obj);
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x06005148 RID: 20808 RVA: 0x0011CB42 File Offset: 0x0011AD42
		// (set) Token: 0x06005147 RID: 20807 RVA: 0x0011CB39 File Offset: 0x0011AD39
		internal object TopObject
		{
			get
			{
				return this.m_topObject;
			}
			set
			{
				this.m_topObject = value;
			}
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x06005149 RID: 20809 RVA: 0x0011CB4A File Offset: 0x0011AD4A
		internal ObjectHolderList SpecialFixupObjects
		{
			get
			{
				if (this.m_specialFixupObjects == null)
				{
					this.m_specialFixupObjects = new ObjectHolderList();
				}
				return this.m_specialFixupObjects;
			}
		}

		// Token: 0x0600514B RID: 20811 RVA: 0x0011CB7C File Offset: 0x0011AD7C
		internal ObjectHolder FindObjectHolder(long objectID)
		{
			int num = (int)(objectID & (long)this.ArrayMask);
			if (num >= this.m_objects.Length)
			{
				return null;
			}
			ObjectHolder objectHolder;
			for (objectHolder = this.m_objects[num]; objectHolder != null; objectHolder = objectHolder.m_next)
			{
				if (objectHolder.m_id == objectID)
				{
					return objectHolder;
				}
			}
			return objectHolder;
		}

		// Token: 0x0600514C RID: 20812 RVA: 0x0011CBC4 File Offset: 0x0011ADC4
		internal ObjectHolder FindOrCreateObjectHolder(long objectID)
		{
			ObjectHolder objectHolder = this.FindObjectHolder(objectID);
			if (objectHolder == null)
			{
				objectHolder = new ObjectHolder(objectID);
				this.AddObjectHolder(objectHolder);
			}
			return objectHolder;
		}

		// Token: 0x0600514D RID: 20813 RVA: 0x0011CBEC File Offset: 0x0011ADEC
		private void AddObjectHolder(ObjectHolder holder)
		{
			if (holder.m_id >= (long)this.m_objects.Length && this.m_objects.Length != this.MaxArraySize)
			{
				int num = this.MaxArraySize;
				if (holder.m_id < (long)(this.MaxArraySize / 2))
				{
					num = this.m_objects.Length * 2;
					while ((long)num <= holder.m_id && num < this.MaxArraySize)
					{
						num *= 2;
					}
					if (num > this.MaxArraySize)
					{
						num = this.MaxArraySize;
					}
				}
				ObjectHolder[] array = new ObjectHolder[num];
				Array.Copy(this.m_objects, array, this.m_objects.Length);
				this.m_objects = array;
			}
			int num2 = (int)(holder.m_id & (long)this.ArrayMask);
			ObjectHolder next = this.m_objects[num2];
			holder.m_next = next;
			this.m_objects[num2] = holder;
		}

		// Token: 0x0600514E RID: 20814 RVA: 0x0011CCB4 File Offset: 0x0011AEB4
		private bool GetCompletionInfo(FixupHolder fixup, out ObjectHolder holder, out object member, bool bThrowIfMissing)
		{
			member = fixup.m_fixupInfo;
			holder = this.FindObjectHolder(fixup.m_id);
			if (!holder.CompletelyFixed && holder.ObjectValue != null && holder.ObjectValue is ValueType)
			{
				this.SpecialFixupObjects.Add(holder);
				return false;
			}
			if (holder != null && !holder.CanObjectValueChange && holder.ObjectValue != null)
			{
				return true;
			}
			if (!bThrowIfMissing)
			{
				return false;
			}
			if (holder == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_NeverSeen", new object[]
				{
					fixup.m_id
				}));
			}
			if (holder.IsIncompleteObjectReference)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_IORIncomplete", new object[]
				{
					fixup.m_id
				}));
			}
			throw new SerializationException(Environment.GetResourceString("Serialization_ObjectNotSupplied", new object[]
			{
				fixup.m_id
			}));
		}

		// Token: 0x0600514F RID: 20815 RVA: 0x0011CD9C File Offset: 0x0011AF9C
		[SecurityCritical]
		private void FixupSpecialObject(ObjectHolder holder)
		{
			ISurrogateSelector selector = null;
			if (holder.HasSurrogate)
			{
				ISerializationSurrogate surrogate = holder.Surrogate;
				object obj = surrogate.SetObjectData(holder.ObjectValue, holder.SerializationInfo, this.m_context, selector);
				if (obj != null)
				{
					if (!holder.CanSurrogatedObjectValueChange && obj != holder.ObjectValue)
					{
						throw new SerializationException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Serialization_NotCyclicallyReferenceableSurrogate"), surrogate.GetType().FullName));
					}
					holder.SetObjectValue(obj, this);
				}
				holder.m_surrogate = null;
				holder.SetFlags();
			}
			else
			{
				this.CompleteISerializableObject(holder.ObjectValue, holder.SerializationInfo, this.m_context);
			}
			holder.SerializationInfo = null;
			holder.RequiresSerInfoFixup = false;
			if (holder.RequiresValueTypeFixup && holder.ValueTypeFixupPerformed)
			{
				this.DoValueTypeFixup(null, holder, holder.ObjectValue);
			}
			this.DoNewlyRegisteredObjectFixups(holder);
		}

		// Token: 0x06005150 RID: 20816 RVA: 0x0011CE70 File Offset: 0x0011B070
		[SecurityCritical]
		private bool ResolveObjectReference(ObjectHolder holder)
		{
			int num = 0;
			try
			{
				object objectValue;
				for (;;)
				{
					objectValue = holder.ObjectValue;
					holder.SetObjectValue(((IObjectReference)holder.ObjectValue).GetRealObject(this.m_context), this);
					if (holder.ObjectValue == null)
					{
						break;
					}
					if (num++ == 100)
					{
						goto Block_3;
					}
					if (!(holder.ObjectValue is IObjectReference) || objectValue == holder.ObjectValue)
					{
						goto IL_69;
					}
				}
				holder.SetObjectValue(objectValue, this);
				return false;
				Block_3:
				throw new SerializationException(Environment.GetResourceString("Serialization_TooManyReferences"));
				IL_69:;
			}
			catch (NullReferenceException)
			{
				return false;
			}
			holder.IsIncompleteObjectReference = false;
			this.DoNewlyRegisteredObjectFixups(holder);
			return true;
		}

		// Token: 0x06005151 RID: 20817 RVA: 0x0011CF10 File Offset: 0x0011B110
		[SecurityCritical]
		private bool DoValueTypeFixup(FieldInfo memberToFix, ObjectHolder holder, object value)
		{
			FieldInfo[] array = new FieldInfo[4];
			int num = 0;
			int[] array2 = null;
			object objectValue = holder.ObjectValue;
			while (holder.RequiresValueTypeFixup)
			{
				if (num + 1 >= array.Length)
				{
					FieldInfo[] array3 = new FieldInfo[array.Length * 2];
					Array.Copy(array, array3, array.Length);
					array = array3;
				}
				ValueTypeFixupInfo valueFixup = holder.ValueFixup;
				objectValue = holder.ObjectValue;
				if (valueFixup.ParentField != null)
				{
					FieldInfo parentField = valueFixup.ParentField;
					ObjectHolder objectHolder = this.FindObjectHolder(valueFixup.ContainerID);
					if (objectHolder.ObjectValue == null)
					{
						break;
					}
					if (Nullable.GetUnderlyingType(parentField.FieldType) != null)
					{
						array[num] = parentField.FieldType.GetField("value", BindingFlags.Instance | BindingFlags.NonPublic);
						num++;
					}
					array[num] = parentField;
					holder = objectHolder;
					num++;
				}
				else
				{
					holder = this.FindObjectHolder(valueFixup.ContainerID);
					array2 = valueFixup.ParentIndex;
					if (holder.ObjectValue == null)
					{
						break;
					}
					break;
				}
			}
			if (!(holder.ObjectValue is Array) && holder.ObjectValue != null)
			{
				objectValue = holder.ObjectValue;
			}
			if (num != 0)
			{
				FieldInfo[] array4 = new FieldInfo[num];
				for (int i = 0; i < num; i++)
				{
					FieldInfo fieldInfo = array[num - 1 - i];
					SerializationFieldInfo serializationFieldInfo = fieldInfo as SerializationFieldInfo;
					array4[i] = ((serializationFieldInfo == null) ? fieldInfo : serializationFieldInfo.FieldInfo);
				}
				TypedReference typedReference = TypedReference.MakeTypedReference(objectValue, array4);
				if (memberToFix != null)
				{
					((RuntimeFieldInfo)memberToFix).SetValueDirect(typedReference, value);
				}
				else
				{
					TypedReference.SetTypedReference(typedReference, value);
				}
			}
			else if (memberToFix != null)
			{
				FormatterServices.SerializationSetValue(memberToFix, objectValue, value);
			}
			if (array2 != null && holder.ObjectValue != null)
			{
				((Array)holder.ObjectValue).SetValue(objectValue, array2);
			}
			return true;
		}

		// Token: 0x06005152 RID: 20818 RVA: 0x0011D0CC File Offset: 0x0011B2CC
		[Conditional("SER_LOGGING")]
		private void DumpValueTypeFixup(object obj, FieldInfo[] intermediateFields, FieldInfo memberToFix, object value)
		{
			StringBuilder stringBuilder = new StringBuilder("  " + obj);
			if (intermediateFields != null)
			{
				for (int i = 0; i < intermediateFields.Length; i++)
				{
					stringBuilder.Append("." + intermediateFields[i].Name);
				}
			}
			stringBuilder.Append(string.Concat(new object[]
			{
				".",
				memberToFix.Name,
				"=",
				value
			}));
		}

		// Token: 0x06005153 RID: 20819 RVA: 0x0011D144 File Offset: 0x0011B344
		[SecurityCritical]
		internal void CompleteObject(ObjectHolder holder, bool bObjectFullyComplete)
		{
			FixupHolderList missingElements = holder.m_missingElements;
			object obj = null;
			ObjectHolder objectHolder = null;
			int num = 0;
			if (holder.ObjectValue == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_MissingObject", new object[]
				{
					holder.m_id
				}));
			}
			if (missingElements == null)
			{
				return;
			}
			if (holder.HasSurrogate || holder.HasISerializable)
			{
				SerializationInfo serInfo = holder.m_serInfo;
				if (serInfo == null)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidFixupDiscovered"));
				}
				if (missingElements != null)
				{
					for (int i = 0; i < missingElements.m_count; i++)
					{
						if (missingElements.m_values[i] != null && this.GetCompletionInfo(missingElements.m_values[i], out objectHolder, out obj, bObjectFullyComplete))
						{
							object objectValue = objectHolder.ObjectValue;
							if (this.CanCallGetType(objectValue))
							{
								serInfo.UpdateValue((string)obj, objectValue, objectValue.GetType());
							}
							else
							{
								serInfo.UpdateValue((string)obj, objectValue, typeof(MarshalByRefObject));
							}
							num++;
							missingElements.m_values[i] = null;
							if (!bObjectFullyComplete)
							{
								holder.DecrementFixupsRemaining(this);
								objectHolder.RemoveDependency(holder.m_id);
							}
						}
					}
				}
			}
			else
			{
				for (int j = 0; j < missingElements.m_count; j++)
				{
					FixupHolder fixupHolder = missingElements.m_values[j];
					if (fixupHolder != null && this.GetCompletionInfo(fixupHolder, out objectHolder, out obj, bObjectFullyComplete))
					{
						if (objectHolder.TypeLoadExceptionReachable)
						{
							holder.TypeLoadException = objectHolder.TypeLoadException;
							if (holder.Reachable)
							{
								throw new SerializationException(Environment.GetResourceString("Serialization_TypeLoadFailure", new object[]
								{
									holder.TypeLoadException.TypeName
								}));
							}
						}
						if (holder.Reachable)
						{
							objectHolder.Reachable = true;
						}
						int fixupType = fixupHolder.m_fixupType;
						if (fixupType != 1)
						{
							if (fixupType != 2)
							{
								throw new SerializationException(Environment.GetResourceString("Serialization_UnableToFixup"));
							}
							MemberInfo memberInfo = (MemberInfo)obj;
							if (memberInfo.MemberType != MemberTypes.Field)
							{
								throw new SerializationException(Environment.GetResourceString("Serialization_UnableToFixup"));
							}
							if (holder.RequiresValueTypeFixup && holder.ValueTypeFixupPerformed)
							{
								if (!this.DoValueTypeFixup((FieldInfo)memberInfo, holder, objectHolder.ObjectValue))
								{
									throw new SerializationException(Environment.GetResourceString("Serialization_PartialValueTypeFixup"));
								}
							}
							else
							{
								FormatterServices.SerializationSetValue(memberInfo, holder.ObjectValue, objectHolder.ObjectValue);
							}
							if (objectHolder.RequiresValueTypeFixup)
							{
								objectHolder.ValueTypeFixupPerformed = true;
							}
						}
						else
						{
							if (holder.RequiresValueTypeFixup)
							{
								throw new SerializationException(Environment.GetResourceString("Serialization_ValueTypeFixup"));
							}
							((Array)holder.ObjectValue).SetValue(objectHolder.ObjectValue, (int[])obj);
						}
						num++;
						missingElements.m_values[j] = null;
						if (!bObjectFullyComplete)
						{
							holder.DecrementFixupsRemaining(this);
							objectHolder.RemoveDependency(holder.m_id);
						}
					}
				}
			}
			this.m_fixupCount -= (long)num;
			if (missingElements.m_count == num)
			{
				holder.m_missingElements = null;
			}
		}

		// Token: 0x06005154 RID: 20820 RVA: 0x0011D424 File Offset: 0x0011B624
		[SecurityCritical]
		private void DoNewlyRegisteredObjectFixups(ObjectHolder holder)
		{
			if (holder.CanObjectValueChange)
			{
				return;
			}
			LongList dependentObjects = holder.DependentObjects;
			if (dependentObjects == null)
			{
				return;
			}
			dependentObjects.StartEnumeration();
			while (dependentObjects.MoveNext())
			{
				long objectID = dependentObjects.Current;
				ObjectHolder objectHolder = this.FindObjectHolder(objectID);
				objectHolder.DecrementFixupsRemaining(this);
				if (objectHolder.DirectlyDependentObjects == 0)
				{
					if (objectHolder.ObjectValue != null)
					{
						this.CompleteObject(objectHolder, true);
					}
					else
					{
						objectHolder.MarkForCompletionWhenAvailable();
					}
				}
			}
		}

		// Token: 0x06005155 RID: 20821 RVA: 0x0011D48C File Offset: 0x0011B68C
		public virtual object GetObject(long objectID)
		{
			if (objectID <= 0L)
			{
				throw new ArgumentOutOfRangeException("objectID", Environment.GetResourceString("ArgumentOutOfRange_ObjectID"));
			}
			ObjectHolder objectHolder = this.FindObjectHolder(objectID);
			if (objectHolder == null || objectHolder.CanObjectValueChange)
			{
				return null;
			}
			return objectHolder.ObjectValue;
		}

		// Token: 0x06005156 RID: 20822 RVA: 0x0011D4CE File Offset: 0x0011B6CE
		[SecurityCritical]
		public virtual void RegisterObject(object obj, long objectID)
		{
			this.RegisterObject(obj, objectID, null, 0L, null);
		}

		// Token: 0x06005157 RID: 20823 RVA: 0x0011D4DC File Offset: 0x0011B6DC
		[SecurityCritical]
		public void RegisterObject(object obj, long objectID, SerializationInfo info)
		{
			this.RegisterObject(obj, objectID, info, 0L, null);
		}

		// Token: 0x06005158 RID: 20824 RVA: 0x0011D4EA File Offset: 0x0011B6EA
		[SecurityCritical]
		public void RegisterObject(object obj, long objectID, SerializationInfo info, long idOfContainingObj, MemberInfo member)
		{
			this.RegisterObject(obj, objectID, info, idOfContainingObj, member, null);
		}

		// Token: 0x06005159 RID: 20825 RVA: 0x0011D4FC File Offset: 0x0011B6FC
		internal void RegisterString(string obj, long objectID, SerializationInfo info, long idOfContainingObj, MemberInfo member)
		{
			ObjectHolder holder = new ObjectHolder(obj, objectID, info, null, idOfContainingObj, (FieldInfo)member, null);
			this.AddObjectHolder(holder);
		}

		// Token: 0x0600515A RID: 20826 RVA: 0x0011D524 File Offset: 0x0011B724
		[SecurityCritical]
		public void RegisterObject(object obj, long objectID, SerializationInfo info, long idOfContainingObj, MemberInfo member, int[] arrayIndex)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (objectID <= 0L)
			{
				throw new ArgumentOutOfRangeException("objectID", Environment.GetResourceString("ArgumentOutOfRange_ObjectID"));
			}
			if (member != null && !(member is RuntimeFieldInfo) && !(member is SerializationFieldInfo))
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_UnknownMemberInfo"));
			}
			ISerializationSurrogate surrogate = null;
			if (this.m_selector != null)
			{
				Type type;
				if (this.CanCallGetType(obj))
				{
					type = obj.GetType();
				}
				else
				{
					type = typeof(MarshalByRefObject);
				}
				ISurrogateSelector surrogateSelector;
				surrogate = this.m_selector.GetSurrogate(type, this.m_context, out surrogateSelector);
			}
			if (obj is IDeserializationCallback)
			{
				DeserializationEventHandler handler = new DeserializationEventHandler(((IDeserializationCallback)obj).OnDeserialization);
				this.AddOnDeserialization(handler);
			}
			if (arrayIndex != null)
			{
				arrayIndex = (int[])arrayIndex.Clone();
			}
			ObjectHolder objectHolder = this.FindObjectHolder(objectID);
			if (objectHolder == null)
			{
				objectHolder = new ObjectHolder(obj, objectID, info, surrogate, idOfContainingObj, (FieldInfo)member, arrayIndex);
				this.AddObjectHolder(objectHolder);
				if (objectHolder.RequiresDelayedFixup)
				{
					this.SpecialFixupObjects.Add(objectHolder);
				}
				this.AddOnDeserialized(obj);
				return;
			}
			if (objectHolder.ObjectValue != null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_RegisterTwice"));
			}
			objectHolder.UpdateData(obj, info, surrogate, idOfContainingObj, (FieldInfo)member, arrayIndex, this);
			if (objectHolder.DirectlyDependentObjects > 0)
			{
				this.CompleteObject(objectHolder, false);
			}
			if (objectHolder.RequiresDelayedFixup)
			{
				this.SpecialFixupObjects.Add(objectHolder);
			}
			if (objectHolder.CompletelyFixed)
			{
				this.DoNewlyRegisteredObjectFixups(objectHolder);
				objectHolder.DependentObjects = null;
			}
			if (objectHolder.TotalDependentObjects > 0)
			{
				this.AddOnDeserialized(obj);
				return;
			}
			this.RaiseOnDeserializedEvent(obj);
		}

		// Token: 0x0600515B RID: 20827 RVA: 0x0011D6BC File Offset: 0x0011B8BC
		[SecurityCritical]
		internal void CompleteISerializableObject(object obj, SerializationInfo info, StreamingContext context)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (!(obj is ISerializable))
			{
				throw new ArgumentException(Environment.GetResourceString("Serialization_NotISer"));
			}
			RuntimeConstructorInfo runtimeConstructorInfo = null;
			RuntimeType runtimeType = (RuntimeType)obj.GetType();
			try
			{
				if (runtimeType == ObjectManager.TypeOfWindowsIdentity && this.m_isCrossAppDomain)
				{
					runtimeConstructorInfo = WindowsIdentity.GetSpecialSerializationCtor();
				}
				else
				{
					runtimeConstructorInfo = ObjectManager.GetConstructor(runtimeType);
				}
			}
			catch (Exception innerException)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_ConstructorNotFound", new object[]
				{
					runtimeType
				}), innerException);
			}
			runtimeConstructorInfo.SerializationInvoke(obj, info, context);
		}

		// Token: 0x0600515C RID: 20828 RVA: 0x0011D758 File Offset: 0x0011B958
		internal static RuntimeConstructorInfo GetConstructor(RuntimeType t)
		{
			RuntimeConstructorInfo serializationCtor = t.GetSerializationCtor();
			if (serializationCtor == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_ConstructorNotFound", new object[]
				{
					t.FullName
				}));
			}
			return serializationCtor;
		}

		// Token: 0x0600515D RID: 20829 RVA: 0x0011D798 File Offset: 0x0011B998
		[SecuritySafeCritical]
		public virtual void DoFixups()
		{
			int num = -1;
			while (num != 0)
			{
				num = 0;
				ObjectHolderListEnumerator fixupEnumerator = this.SpecialFixupObjects.GetFixupEnumerator();
				while (fixupEnumerator.MoveNext())
				{
					ObjectHolder objectHolder = fixupEnumerator.Current;
					if (objectHolder.ObjectValue == null)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_ObjectNotSupplied", new object[]
						{
							objectHolder.m_id
						}));
					}
					if (objectHolder.TotalDependentObjects == 0)
					{
						if (objectHolder.RequiresSerInfoFixup)
						{
							this.FixupSpecialObject(objectHolder);
							num++;
						}
						else if (!objectHolder.IsIncompleteObjectReference)
						{
							this.CompleteObject(objectHolder, true);
						}
						if (objectHolder.IsIncompleteObjectReference && this.ResolveObjectReference(objectHolder))
						{
							num++;
						}
					}
				}
			}
			if (this.m_fixupCount != 0L)
			{
				for (int i = 0; i < this.m_objects.Length; i++)
				{
					for (ObjectHolder objectHolder = this.m_objects[i]; objectHolder != null; objectHolder = objectHolder.m_next)
					{
						if (objectHolder.TotalDependentObjects > 0)
						{
							this.CompleteObject(objectHolder, true);
						}
					}
					if (this.m_fixupCount == 0L)
					{
						return;
					}
				}
				throw new SerializationException(Environment.GetResourceString("Serialization_IncorrectNumberOfFixups"));
			}
			if (this.TopObject is TypeLoadExceptionHolder)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_TypeLoadFailure", new object[]
				{
					((TypeLoadExceptionHolder)this.TopObject).TypeName
				}));
			}
		}

		// Token: 0x0600515E RID: 20830 RVA: 0x0011D8D0 File Offset: 0x0011BAD0
		private void RegisterFixup(FixupHolder fixup, long objectToBeFixed, long objectRequired)
		{
			ObjectHolder objectHolder = this.FindOrCreateObjectHolder(objectToBeFixed);
			if (objectHolder.RequiresSerInfoFixup && fixup.m_fixupType == 2)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InvalidFixupType"));
			}
			objectHolder.AddFixup(fixup, this);
			ObjectHolder objectHolder2 = this.FindOrCreateObjectHolder(objectRequired);
			objectHolder2.AddDependency(objectToBeFixed);
			this.m_fixupCount += 1L;
		}

		// Token: 0x0600515F RID: 20831 RVA: 0x0011D92C File Offset: 0x0011BB2C
		public virtual void RecordFixup(long objectToBeFixed, MemberInfo member, long objectRequired)
		{
			if (objectToBeFixed <= 0L || objectRequired <= 0L)
			{
				throw new ArgumentOutOfRangeException((objectToBeFixed <= 0L) ? "objectToBeFixed" : "objectRequired", Environment.GetResourceString("Serialization_IdTooSmall"));
			}
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			if (!(member is RuntimeFieldInfo) && !(member is SerializationFieldInfo))
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InvalidType", new object[]
				{
					member.GetType().ToString()
				}));
			}
			FixupHolder fixup = new FixupHolder(objectRequired, member, 2);
			this.RegisterFixup(fixup, objectToBeFixed, objectRequired);
		}

		// Token: 0x06005160 RID: 20832 RVA: 0x0011D9C0 File Offset: 0x0011BBC0
		public virtual void RecordDelayedFixup(long objectToBeFixed, string memberName, long objectRequired)
		{
			if (objectToBeFixed <= 0L || objectRequired <= 0L)
			{
				throw new ArgumentOutOfRangeException((objectToBeFixed <= 0L) ? "objectToBeFixed" : "objectRequired", Environment.GetResourceString("Serialization_IdTooSmall"));
			}
			if (memberName == null)
			{
				throw new ArgumentNullException("memberName");
			}
			FixupHolder fixup = new FixupHolder(objectRequired, memberName, 4);
			this.RegisterFixup(fixup, objectToBeFixed, objectRequired);
		}

		// Token: 0x06005161 RID: 20833 RVA: 0x0011DA18 File Offset: 0x0011BC18
		public virtual void RecordArrayElementFixup(long arrayToBeFixed, int index, long objectRequired)
		{
			this.RecordArrayElementFixup(arrayToBeFixed, new int[]
			{
				index
			}, objectRequired);
		}

		// Token: 0x06005162 RID: 20834 RVA: 0x0011DA3C File Offset: 0x0011BC3C
		public virtual void RecordArrayElementFixup(long arrayToBeFixed, int[] indices, long objectRequired)
		{
			if (arrayToBeFixed <= 0L || objectRequired <= 0L)
			{
				throw new ArgumentOutOfRangeException((arrayToBeFixed <= 0L) ? "objectToBeFixed" : "objectRequired", Environment.GetResourceString("Serialization_IdTooSmall"));
			}
			if (indices == null)
			{
				throw new ArgumentNullException("indices");
			}
			FixupHolder fixup = new FixupHolder(objectRequired, indices, 1);
			this.RegisterFixup(fixup, arrayToBeFixed, objectRequired);
		}

		// Token: 0x06005163 RID: 20835 RVA: 0x0011DA94 File Offset: 0x0011BC94
		public virtual void RaiseDeserializationEvent()
		{
			if (this.m_onDeserializedHandler != null)
			{
				this.m_onDeserializedHandler(this.m_context);
			}
			if (this.m_onDeserializationHandler != null)
			{
				this.m_onDeserializationHandler(null);
			}
		}

		// Token: 0x06005164 RID: 20836 RVA: 0x0011DAC3 File Offset: 0x0011BCC3
		internal virtual void AddOnDeserialization(DeserializationEventHandler handler)
		{
			this.m_onDeserializationHandler = (DeserializationEventHandler)Delegate.Combine(this.m_onDeserializationHandler, handler);
		}

		// Token: 0x06005165 RID: 20837 RVA: 0x0011DADC File Offset: 0x0011BCDC
		internal virtual void RemoveOnDeserialization(DeserializationEventHandler handler)
		{
			this.m_onDeserializationHandler = (DeserializationEventHandler)Delegate.Remove(this.m_onDeserializationHandler, handler);
		}

		// Token: 0x06005166 RID: 20838 RVA: 0x0011DAF8 File Offset: 0x0011BCF8
		[SecuritySafeCritical]
		internal virtual void AddOnDeserialized(object obj)
		{
			SerializationEvents serializationEventsForType = SerializationEventsCache.GetSerializationEventsForType(obj.GetType());
			this.m_onDeserializedHandler = serializationEventsForType.AddOnDeserialized(obj, this.m_onDeserializedHandler);
		}

		// Token: 0x06005167 RID: 20839 RVA: 0x0011DB24 File Offset: 0x0011BD24
		internal virtual void RaiseOnDeserializedEvent(object obj)
		{
			SerializationEvents serializationEventsForType = SerializationEventsCache.GetSerializationEventsForType(obj.GetType());
			serializationEventsForType.InvokeOnDeserialized(obj, this.m_context);
		}

		// Token: 0x06005168 RID: 20840 RVA: 0x0011DB4C File Offset: 0x0011BD4C
		public void RaiseOnDeserializingEvent(object obj)
		{
			SerializationEvents serializationEventsForType = SerializationEventsCache.GetSerializationEventsForType(obj.GetType());
			serializationEventsForType.InvokeOnDeserializing(obj, this.m_context);
		}

		// Token: 0x040023BF RID: 9151
		private const int DefaultInitialSize = 16;

		// Token: 0x040023C0 RID: 9152
		private const int DefaultMaxArraySize = 4096;

		// Token: 0x040023C1 RID: 9153
		private const int NewMaxArraySize = 16777216;

		// Token: 0x040023C2 RID: 9154
		private const int MaxReferenceDepth = 100;

		// Token: 0x040023C3 RID: 9155
		private DeserializationEventHandler m_onDeserializationHandler;

		// Token: 0x040023C4 RID: 9156
		private SerializationEventHandler m_onDeserializedHandler;

		// Token: 0x040023C5 RID: 9157
		private static RuntimeType TypeOfWindowsIdentity = (RuntimeType)typeof(WindowsIdentity);

		// Token: 0x040023C6 RID: 9158
		internal ObjectHolder[] m_objects;

		// Token: 0x040023C7 RID: 9159
		internal object m_topObject;

		// Token: 0x040023C8 RID: 9160
		internal ObjectHolderList m_specialFixupObjects;

		// Token: 0x040023C9 RID: 9161
		internal long m_fixupCount;

		// Token: 0x040023CA RID: 9162
		internal ISurrogateSelector m_selector;

		// Token: 0x040023CB RID: 9163
		internal StreamingContext m_context;

		// Token: 0x040023CC RID: 9164
		private bool m_isCrossAppDomain;

		// Token: 0x040023CD RID: 9165
		private readonly int MaxArraySize;

		// Token: 0x040023CE RID: 9166
		private readonly int ArrayMask;
	}
}
