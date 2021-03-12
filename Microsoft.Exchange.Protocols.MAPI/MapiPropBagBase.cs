using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Mapi;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000010 RID: 16
	public abstract class MapiPropBagBase : MapiBase
	{
		// Token: 0x0600005B RID: 91 RVA: 0x000033CF File Offset: 0x000015CF
		internal MapiPropBagBase(MapiObjectType mapiObjectType) : base(mapiObjectType)
		{
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005C RID: 92
		protected abstract PropertyBag StorePropertyBag { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000033D8 File Offset: 0x000015D8
		// (set) Token: 0x0600005E RID: 94 RVA: 0x000033F7 File Offset: 0x000015F7
		public virtual bool IsReadOnly
		{
			get
			{
				MapiPropBagBase parentObject = base.ParentObject;
				return parentObject != null && parentObject.IsReadOnly;
			}
			set
			{
				DiagnosticContext.TraceLocation((LID)47976U);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003408 File Offset: 0x00001608
		public List<MapiBase> SubobjectsForTest
		{
			get
			{
				return this.subObjects;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003410 File Offset: 0x00001610
		// (set) Token: 0x06000061 RID: 97 RVA: 0x0000341D File Offset: 0x0000161D
		public bool NoReplicateOperationInProgress
		{
			get
			{
				return this.StorePropertyBag.NoReplicateOperationInProgress;
			}
			set
			{
				this.StorePropertyBag.NoReplicateOperationInProgress = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000062 RID: 98 RVA: 0x0000342B File Offset: 0x0000162B
		public virtual bool CanUseSharedMailboxLockForCopy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000342E File Offset: 0x0000162E
		public MapiPropBagBase.PropertyReader GetPropsReader(MapiContext context, IList<StorePropTag> propTags)
		{
			base.ThrowIfNotValid(null);
			this.CheckPropertyRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadProperty, AccessCheckOperation.PropertyGet, (LID)44167U);
			return new MapiPropBagBase.PropertyReader(this, propTags);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003454 File Offset: 0x00001654
		internal Properties GetProps(MapiContext context, IList<StorePropTag> propTags)
		{
			MapiPropBagBase.PropertyReader propsReader = this.GetPropsReader(context, propTags);
			Properties result = new Properties(propsReader.PropertyCount);
			Property prop;
			while (propsReader.ReadNext(context, out prop))
			{
				result.Add(prop);
			}
			return result;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003490 File Offset: 0x00001690
		public Property GetOneProp(MapiContext context, StorePropTag propTag)
		{
			base.ThrowIfNotValid(null);
			this.CheckPropertyRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadProperty, AccessCheckOperation.PropertyGet, (LID)60551U);
			ErrorCode first = this.CheckPropertyOperationAllowed(context, MapiPropBagBase.PropOperation.GetProps, propTag, null);
			if (first != ErrorCode.NoError)
			{
				if (ExTraceGlobals.GetPropsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.GetPropsPropertiesTracer.TraceDebug<StorePropTag>(0L, "GetProp blocked: tag:[{0}]", propTag);
				}
				return new Property(propTag.ConvertToError(), LegacyHelper.BoxedErrorCodeNotFound);
			}
			Property property;
			if (MapiPropBagBase.getPropTestHook.Value != null)
			{
				property = MapiPropBagBase.getPropTestHook.Value(propTag);
			}
			else
			{
				property = this.InternalGetOneProp(context, propTag);
			}
			if (ExTraceGlobals.GetPropsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.GetPropsPropertiesTracer.TraceDebug<Property>(0L, "GetProp: {0}", property);
			}
			return property;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003548 File Offset: 0x00001748
		public object GetOnePropValue(MapiContext context, StorePropTag propTag)
		{
			base.ThrowIfNotValid(null);
			Property oneProp = this.GetOneProp(context, propTag);
			if (oneProp.IsError)
			{
				return null;
			}
			return oneProp.Value;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003578 File Offset: 0x00001778
		public ErrorCode SetOneProp(MapiContext context, StorePropTag propTag, object value)
		{
			base.ThrowIfNotValid(null);
			this.CheckPropertyRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteProperty, AccessCheckOperation.PropertySet, (LID)35975U);
			ErrorCode errorCode = this.CheckPropertyOperationAllowed(context, MapiPropBagBase.PropOperation.SetProps, propTag, value).Propagate((LID)36560U);
			if (errorCode == ErrorCode.NoError)
			{
				if (MapiPropBagBase.setPropTestHook.Value != null)
				{
					errorCode = MapiPropBagBase.setPropTestHook.Value(propTag, value);
				}
				else
				{
					errorCode = this.InternalSetOneProp(context, propTag, value);
				}
			}
			if (errorCode != ErrorCode.NoError)
			{
				if (propTag.IsCategory(11))
				{
					if (ExTraceGlobals.SetPropsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.SetPropsPropertiesTracer.TraceDebug<ErrorCode, Property>(0L, "SetProp ignoring error {0}: {1}", errorCode, new Property(propTag, value));
					}
					errorCode = ErrorCode.NoError;
				}
				else if (ExTraceGlobals.SetPropsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.SetPropsPropertiesTracer.TraceDebug<ErrorCode, Property>(0L, "SetProp error {0}: {1}", errorCode, new Property(propTag, value));
				}
			}
			else if (ExTraceGlobals.SetPropsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.SetPropsPropertiesTracer.TraceDebug<Property>(0L, "SetProp: {0}", new Property(propTag, value));
			}
			return errorCode;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003688 File Offset: 0x00001888
		public virtual void SetProps(MapiContext context, Properties properties, ref List<MapiPropertyProblem> propProblems)
		{
			base.ThrowIfNotValid(null);
			this.CheckPropertyRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteProperty, AccessCheckOperation.PropertySet, (LID)52359U);
			bool flag = ExTraceGlobals.SetPropsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace);
			for (int i = 0; i < properties.Count; i++)
			{
				StorePropTag tag = properties[i].Tag;
				ErrorCode errorCode = this.CheckPropertyOperationAllowed(context, MapiPropBagBase.PropOperation.SetProps, tag, properties[i].Value).Propagate((LID)41064U);
				if (errorCode == ErrorCode.NoError)
				{
					if (MapiPropBagBase.setPropTestHook.Value != null)
					{
						errorCode = MapiPropBagBase.setPropTestHook.Value(tag, properties[i].Value);
					}
					else
					{
						errorCode = this.InternalSetOneProp(context, tag, properties[i].Value);
					}
				}
				if (errorCode != ErrorCode.NoError)
				{
					if (!tag.IsCategory(11))
					{
						if (flag)
						{
							ExTraceGlobals.SetPropsPropertiesTracer.TraceDebug<ErrorCode, Property>(0L, "SetProp error {0}: {1}", errorCode, properties[i]);
						}
						MapiPropertyProblem item = default(MapiPropertyProblem);
						item.MapiPropTag = tag;
						item.ErrorCode = errorCode;
						if (propProblems == null)
						{
							propProblems = new List<MapiPropertyProblem>();
						}
						propProblems.Add(item);
					}
					else if (flag)
					{
						ExTraceGlobals.SetPropsPropertiesTracer.TraceDebug<ErrorCode, Property>(0L, "SetProp ignoring error {0}: {1}", errorCode, properties[i]);
					}
				}
				else if (flag)
				{
					ExTraceGlobals.SetPropsPropertiesTracer.TraceDebug<Property>(0L, "SetProp: {0}", properties[i]);
				}
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003810 File Offset: 0x00001A10
		public ErrorCode DeleteOneProp(MapiContext context, StorePropTag propTag)
		{
			base.ThrowIfNotValid(null);
			this.CheckPropertyRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteProperty, AccessCheckOperation.PropertyDelete, (LID)44391U);
			ErrorCode errorCode = this.CheckPropertyOperationAllowed(context, MapiPropBagBase.PropOperation.DeleteProps, propTag, null).Propagate((LID)45160U);
			if (errorCode == ErrorCode.NoError)
			{
				if (MapiPropBagBase.setPropTestHook.Value != null)
				{
					errorCode = MapiPropBagBase.setPropTestHook.Value(propTag, null);
				}
				else
				{
					errorCode = this.InternalDeleteOneProp(context, propTag);
				}
			}
			if (errorCode != ErrorCode.NoError)
			{
				if (ExTraceGlobals.DeletePropsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.DeletePropsPropertiesTracer.TraceDebug<ErrorCode, StorePropTag>(0L, "DeleteProp error {0}: tag:[{1}]", errorCode, propTag);
				}
			}
			else if (ExTraceGlobals.DeletePropsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.DeletePropsPropertiesTracer.TraceDebug<StorePropTag>(0L, "DeleteProp: tag:[{0}]", propTag);
			}
			return errorCode;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000038DC File Offset: 0x00001ADC
		public virtual void DeleteProps(MapiContext context, StorePropTag[] propTags, ref List<MapiPropertyProblem> propProblems)
		{
			base.ThrowIfNotValid(null);
			this.CheckPropertyRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteProperty, AccessCheckOperation.PropertyDelete, (LID)60775U);
			bool flag = ExTraceGlobals.DeletePropsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace);
			foreach (StorePropTag storePropTag in propTags)
			{
				ErrorCode errorCode = this.CheckPropertyOperationAllowed(context, MapiPropBagBase.PropOperation.DeleteProps, storePropTag, null).Propagate((LID)36904U);
				if (errorCode == ErrorCode.NoError)
				{
					if (MapiPropBagBase.setPropTestHook.Value != null)
					{
						errorCode = MapiPropBagBase.setPropTestHook.Value(storePropTag, null);
					}
					else
					{
						errorCode = this.InternalDeleteOneProp(context, storePropTag);
					}
				}
				if (errorCode != ErrorCode.NoError)
				{
					if (flag)
					{
						ExTraceGlobals.DeletePropsPropertiesTracer.TraceDebug<ErrorCode, StorePropTag>(0L, "DeleteProp error {0}: tag:[{1}]", errorCode, storePropTag);
					}
					MapiPropertyProblem item = default(MapiPropertyProblem);
					item.MapiPropTag = storePropTag;
					item.ErrorCode = errorCode;
					if (propProblems == null)
					{
						propProblems = new List<MapiPropertyProblem>();
					}
					propProblems.Add(item);
				}
				else if (flag)
				{
					ExTraceGlobals.DeletePropsPropertiesTracer.TraceDebug<StorePropTag>(0L, "DeleteProp: tag:[{0}]", storePropTag);
				}
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003A0C File Offset: 0x00001C0C
		public List<Property> GetAllProperties(MapiContext context, GetPropListFlags flags, bool loadValue)
		{
			base.ThrowIfNotValid(null);
			this.CheckPropertyRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadProperty, loadValue ? AccessCheckOperation.PropertyGet : AccessCheckOperation.PropertyGetList, (LID)36199U);
			List<Property> list;
			if (MapiPropBagBase.getAllPropertiesTestHook.Value != null)
			{
				list = MapiPropBagBase.getAllPropertiesTestHook.Value();
			}
			else
			{
				List<PropCategory> allowedCategories = new List<PropCategory>();
				List<PropCategory> blockedCategories = new List<PropCategory>();
				if ((flags & GetPropListFlags.FastTransfer) == GetPropListFlags.None)
				{
					blockedCategories.Add(PropCategory.NoGetPropList);
				}
				else
				{
					blockedCategories.Add(PropCategory.NoGetPropListForFastTransfer);
				}
				if (context.ClientType == ClientType.Migration)
				{
					allowedCategories.Add(PropCategory.FacebookProtectedProperties);
				}
				if (base.Logon.IsMoveUser)
				{
					allowedCategories.Add(PropCategory.SetPropAllowedForMailboxMove);
				}
				Predicate<StorePropTag> propertyFilter = (StorePropTag tag) => MapiPropBagBase.GetAllPropertiesPropertyCategoryFilter(tag, allowedCategories, blockedCategories);
				list = this.InternalGetAllProperties(context, flags, loadValue, propertyFilter);
			}
			ValueHelper.SortAndRemoveDuplicates<Property>(list, PropertyComparerByTag.Comparer);
			return list;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003AE8 File Offset: 0x00001CE8
		private static bool GetAllPropertiesPropertyCategoryFilter(StorePropTag tag, List<PropCategory> allowedCategories, List<PropCategory> blockedCategories)
		{
			for (int i = 0; i < allowedCategories.Count; i++)
			{
				if (tag.IsCategory((int)allowedCategories[i]))
				{
					if (ExTraceGlobals.GetPropsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.GetPropsPropertiesTracer.TraceDebug<StorePropTag>(0L, "GetAllProperties: specifically allowed tag:[{0}]", tag);
					}
					return true;
				}
			}
			for (int j = 0; j < blockedCategories.Count; j++)
			{
				if (tag.IsCategory((int)blockedCategories[j]))
				{
					if (ExTraceGlobals.GetPropsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.GetPropsPropertiesTracer.TraceDebug<StorePropTag>(0L, "GetAllProperties: specifically blocked tag:[{0}]", tag);
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003B7C File Offset: 0x00001D7C
		public virtual void CopyTo(MapiContext context, MapiPropBagBase destination, IList<StorePropTag> propTagsExclude, CopyToFlags flags, ref List<MapiPropertyProblem> propProblems)
		{
			base.ThrowIfNotValid(null);
			if (base.GetType() != destination.GetType())
			{
				throw new ExExceptionInvalidParameter((LID)45816U, "Invalid destination object type in MapiMessage.CopyTo");
			}
			List<StorePropTag> list;
			if (MapiPropBagBase.getPropListTestHook.Value != null)
			{
				list = MapiPropBagBase.getPropListTestHook.Value();
			}
			else
			{
				list = this.InternalGetPropList(context, GetPropListFlags.None);
			}
			this.CopyToRemoveNoAccessProps(context, destination, list);
			MapiPropBagBase.CopyToRemoveExcludeProps(propTagsExclude, list);
			if (MapiPropBagBase.copyToTestHook.Value != null)
			{
				MapiPropBagBase.copyToTestHook.Value(list);
				return;
			}
			if ((CopyToFlags.DoNotReplaceProperties & flags) != CopyToFlags.None)
			{
				MapiPropBagBase.CopyToRemovePreexistingDestinationProperties(context, destination, list);
			}
			if (list.Count != 0)
			{
				this.CopyToCopyPropertiesToDestination(context, list, destination, ref propProblems);
				if ((CopyToFlags.MoveProperties & flags) != CopyToFlags.None)
				{
					MapiPropBagBase.CopyToRemoveProblemProperties(propProblems, list);
					this.CopyToRemoveSourceProperties(context, list, ref propProblems);
				}
			}
			this.CopyToInternal(context, destination, propTagsExclude, flags, ref propProblems);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003C53 File Offset: 0x00001E53
		public virtual void CopyProps(MapiContext context, MapiPropBagBase destination, IList<StorePropTag> propTags, bool replaceIfExists, ref List<MapiPropertyProblem> propProblems)
		{
			throw this.CreateCopyPropsNotSupportedException((LID)62200U, destination);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003C66 File Offset: 0x00001E66
		public virtual bool IsStreamSizeInvalid(MapiContext context, long size)
		{
			return false;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003C6C File Offset: 0x00001E6C
		public MapiStream OpenStream(MapiContext context, StreamFlags flags, StorePropTag propTag, CodePage codePage)
		{
			MapiStream result;
			ErrorCode errorCode = this.OpenStream(context, flags, propTag, codePage, out result);
			if (errorCode != ErrorCode.NoError)
			{
				throw new StoreException((LID)36704U, errorCode);
			}
			return result;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003CAC File Offset: 0x00001EAC
		public ErrorCode OpenStream(MapiContext context, StreamFlags flags, StorePropTag propTag, CodePage codePage, out MapiStream stream)
		{
			bool flag = flags == StreamFlags.AllowRead;
			MapiStream mapiStream = null;
			MapiStream mapiStream2 = null;
			bool flag2 = false;
			stream = null;
			this.CheckPropertyRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadProperty, AccessCheckOperation.StreamOpen, (LID)52583U);
			if (PropertyType.Unicode != propTag.PropType && PropertyType.Binary != propTag.PropType && PropertyType.Object != propTag.PropType)
			{
				string message = string.Format("MapiPropBagBase:OpenStream(): Only ptags for the following types are supported: PT_UNICODE, PT_STRING8, PT_BINARY, PT_OBJECT. Invalid propTag {0}. throwing ExExceptionInvalidParameter", propTag);
				ExTraceGlobals.GeneralTracer.TraceError(0L, message);
				return ErrorCode.CreateNotSupported((LID)37624U);
			}
			MapiPropBagBase.PropOperation propOperation = ((flags & (StreamFlags.AllowAppend | StreamFlags.AllowWrite)) == (StreamFlags)0) ? MapiPropBagBase.PropOperation.GetProps : MapiPropBagBase.PropOperation.SetProps;
			ErrorCode first = this.CheckPropertyOperationAllowed(context, propOperation, propTag, null).Propagate((LID)45592U);
			if (first != ErrorCode.NoError)
			{
				return first.Propagate((LID)47912U);
			}
			if (this.IsReadOnly && propOperation == MapiPropBagBase.PropOperation.SetProps)
			{
				string message2 = string.Format("MapiPropBagBase:OpenStream(): Cannot open a writable stream on a read-only object. Property {0}.", propTag);
				ExTraceGlobals.GeneralTracer.TraceError(0L, message2);
				return ErrorCode.CreateNoAccess((LID)54008U);
			}
			if (MapiPropBagBase.openStreamTestHook.Value == null)
			{
				if (this.subObjects != null)
				{
					foreach (MapiBase mapiBase in this.subObjects)
					{
						MapiStream mapiStream3 = mapiBase as MapiStream;
						if (mapiStream3 != null && propTag.PropId == mapiStream3.Ptag.PropId && (mapiStream3.ShouldAllowWrite || mapiStream3.ShouldAppend))
						{
							if (!flag)
							{
								return ErrorCode.CreateNotSupported((LID)41720U);
							}
							mapiStream = mapiStream3;
							break;
						}
					}
				}
				ErrorCode noError;
				try
				{
					mapiStream2 = new MapiStream();
					try
					{
						if (mapiStream != null)
						{
							first = mapiStream2.ConfigureStream(context, mapiStream, flags, codePage);
						}
						else
						{
							first = mapiStream2.ConfigureStream(context, this, flags, propTag, codePage);
						}
						if (first != ErrorCode.NoError)
						{
							return first.Propagate((LID)43904U);
						}
					}
					catch (NullColumnException exception)
					{
						context.OnExceptionCatch(exception);
						return ErrorCode.CreateNotFound((LID)58104U);
					}
					flag2 = true;
					stream = mapiStream2;
					noError = ErrorCode.NoError;
				}
				finally
				{
					if (!flag2 && mapiStream2 != null)
					{
						mapiStream2.Dispose();
					}
				}
				return noError;
			}
			stream = MapiPropBagBase.openStreamTestHook.Value(propTag, flags);
			if (stream == null)
			{
				return ErrorCode.CreateNotFound((LID)99999U, propTag.PropTag);
			}
			return ErrorCode.NoError;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003F3C File Offset: 0x0000213C
		internal static IDisposable SetGetPropListTestHook(Func<List<StorePropTag>> callback)
		{
			return MapiPropBagBase.getPropListTestHook.SetTestHook(callback);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003F49 File Offset: 0x00002149
		internal static IDisposable SetGetAllPropertiesTestHook(Func<List<Property>> callback)
		{
			return MapiPropBagBase.getAllPropertiesTestHook.SetTestHook(callback);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003F56 File Offset: 0x00002156
		internal static IDisposable SetGetPropTestHook(Func<StorePropTag, Property> callback)
		{
			return MapiPropBagBase.getPropTestHook.SetTestHook(callback);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003F63 File Offset: 0x00002163
		internal static IDisposable SetSetPropTestHook(Func<StorePropTag, object, ErrorCode> callback)
		{
			return MapiPropBagBase.setPropTestHook.SetTestHook(callback);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003F70 File Offset: 0x00002170
		internal static IDisposable SetOpenStreamTestHook(Func<StorePropTag, StreamFlags, MapiStream> callback)
		{
			return MapiPropBagBase.openStreamTestHook.SetTestHook(callback);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003F7D File Offset: 0x0000217D
		internal static IDisposable SetCopyToTestHook(Action<List<StorePropTag>> callback)
		{
			return MapiPropBagBase.copyToTestHook.SetTestHook(callback);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003F8C File Offset: 0x0000218C
		protected static void CopyToRemoveExcludeProps(IList<StorePropTag> propTagsToExclude, List<StorePropTag> propTagsToCopy)
		{
			if (propTagsToExclude != null && propTagsToExclude.Count != 0)
			{
				for (int i = 0; i < propTagsToCopy.Count; i++)
				{
					bool flag = false;
					for (int j = 0; j < propTagsToExclude.Count; j++)
					{
						if (propTagsToCopy[i].PropTag == propTagsToExclude[j].PropTag)
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						propTagsToCopy.RemoveAt(i);
						i--;
					}
				}
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003FFC File Offset: 0x000021FC
		protected static void CopyToRemovePreexistingDestinationProperties(MapiContext context, MapiPropBagBase destination, List<StorePropTag> propTagsToCopy)
		{
			for (int i = 0; i < propTagsToCopy.Count; i++)
			{
				object onePropValue = destination.GetOnePropValue(context, propTagsToCopy[i]);
				if (onePropValue != null)
				{
					propTagsToCopy.RemoveAt(i);
					i--;
				}
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004038 File Offset: 0x00002238
		protected static void CopyToRemoveInvalidProps(Properties props)
		{
			for (int i = props.Count - 1; i >= 0; i--)
			{
				if (props[i].IsError)
				{
					props.RemoveAt(i);
				}
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004074 File Offset: 0x00002274
		protected static void CopyToRemoveProblemProperties(List<MapiPropertyProblem> propProblems, List<StorePropTag> propTags)
		{
			if (propProblems != null)
			{
				for (int i = 0; i < propProblems.Count; i++)
				{
					propTags.Remove(propProblems[i].MapiPropTag);
				}
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000040A8 File Offset: 0x000022A8
		protected virtual ErrorCode CheckPropertyOperationAllowed(MapiContext context, MapiPropBagBase.PropOperation operation, StorePropTag propTag, object value)
		{
			return MapiPropBagBase.CheckPropertyOperationAllowed(context, base.Logon, false, operation, propTag, value);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000040BC File Offset: 0x000022BC
		internal static ErrorCode CheckPropertyOperationAllowed(MapiContext context, MapiLogon logon, bool isEmbedded, MapiPropBagBase.PropOperation operation, StorePropTag propTag, object value)
		{
			ErrorCode result = ErrorCode.NoError;
			switch (operation)
			{
			case MapiPropBagBase.PropOperation.GetProps:
				if (propTag.IsCategory(0))
				{
					bool flag = false;
					if (propTag.IsCategory(8))
					{
						ClientType clientType = context.ClientType;
						if (clientType != ClientType.OWA)
						{
							switch (clientType)
							{
							case ClientType.Migration:
							case ClientType.TransportSync:
								break;
							default:
								if (clientType != ClientType.EDiscoverySearch)
								{
									goto IL_18B;
								}
								break;
							}
						}
						flag = true;
					}
					IL_18B:
					if (!flag)
					{
						result = ErrorCode.CreateNotFound((LID)50063U, propTag.PropTag);
					}
				}
				break;
			case MapiPropBagBase.PropOperation.SetProps:
			case MapiPropBagBase.PropOperation.DeleteProps:
				if (operation == MapiPropBagBase.PropOperation.SetProps)
				{
					if (propTag.IsNamedProperty && propTag.PropInfo == null)
					{
						result = ErrorCode.CreateUnregisteredNamedProp((LID)65128U, propTag.PropTag);
						break;
					}
					if (propTag.PropType == PropertyType.Error || propTag.PropType == PropertyType.Invalid || propTag.PropType == PropertyType.Unspecified)
					{
						result = ErrorCode.CreateInvalidType((LID)42567U, propTag.PropTag);
						break;
					}
				}
				if (propTag.IsCategory(3))
				{
					bool flag2 = false;
					if ((logon.IsMoveUser || (logon.AdminRights && (context.ClientType == ClientType.Migration || context.ClientType == ClientType.PublicFolderSystem))) && propTag.IsCategory(4))
					{
						flag2 = true;
					}
					else if (logon.AdminRights && propTag.IsCategory(5))
					{
						flag2 = true;
					}
					else if (logon.ExchangeTransportServiceRights && propTag.IsCategory(6))
					{
						flag2 = true;
					}
					else if (isEmbedded && propTag.IsCategory(7))
					{
						flag2 = true;
					}
					if (!flag2)
					{
						if (propTag.IsCategory(10))
						{
							result = ErrorCode.CreateComputed((LID)64024U, propTag.PropTag);
						}
						else
						{
							result = ErrorCode.CreateNoAccess((LID)45653U, propTag.PropTag);
						}
					}
				}
				break;
			}
			return result;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004270 File Offset: 0x00002470
		protected List<StorePropTag> InternalGetPropList(MapiContext context, GetPropListFlags flags)
		{
			base.ThrowIfNotValid(null);
			List<Property> list = this.InternalGetAllProperties(context, flags, false, null);
			List<StorePropTag> list2 = new List<StorePropTag>();
			if (list != null && list.Count > 0)
			{
				list2 = new List<StorePropTag>(list.Count);
				foreach (Property property in list)
				{
					list2.Add(property.Tag);
				}
			}
			return list2;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000432C File Offset: 0x0000252C
		protected virtual List<Property> InternalGetAllProperties(MapiContext context, GetPropListFlags flags, bool loadValues, Predicate<StorePropTag> propertyFilter)
		{
			base.ThrowIfNotValid(null);
			List<Property> propList = new List<Property>(10);
			this.StorePropertyBag.EnumerateProperties(context, delegate(StorePropTag propTag, object propValue)
			{
				if (propertyFilter == null || propertyFilter(propTag))
				{
					propList.Add(new Property(propTag, propValue));
				}
				return true;
			}, loadValues);
			return propList;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000437C File Offset: 0x0000257C
		protected virtual Property InternalGetOneProp(MapiContext context, StorePropTag propTag)
		{
			base.ThrowIfNotValid(null);
			object obj = null;
			StorePropTag tag;
			if (propTag.PropType == PropertyType.Unspecified)
			{
				if (!this.TryGetPropertyImp(context, propTag.PropId, out tag, out obj))
				{
					obj = LegacyHelper.BoxedErrorCodeNotFound;
					tag = propTag.ConvertToError();
				}
			}
			else
			{
				obj = this.GetPropertyValueImp(context, propTag);
				if (obj == null)
				{
					obj = LegacyHelper.BoxedErrorCodeNotFound;
					tag = propTag.ConvertToError();
				}
				else
				{
					tag = propTag;
				}
			}
			MapiProtocolsHelpers.AssertPropValueIsNotSqlType(obj);
			return new Property(tag, obj);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000043EC File Offset: 0x000025EC
		internal void InternalSetPropsShouldNotFail(MapiContext context, Properties properties)
		{
			for (int i = 0; i < properties.Count; i++)
			{
				this.InternalSetOnePropShouldNotFail(context, properties[i].Tag, properties[i].Value);
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004434 File Offset: 0x00002634
		internal void InternalSetOnePropShouldNotFail(MapiContext context, StorePropTag propTag, object value)
		{
			ErrorCode errorCode = this.InternalSetOneProp(context, propTag, value);
			if (errorCode != ErrorCode.NoError)
			{
				throw new ExExceptionInvalidObject((LID)33679U, string.Format("Got a problem on InternalSetOnePropShouldNotFail we should not have gotten: {0}", errorCode));
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004478 File Offset: 0x00002678
		protected virtual ErrorCode InternalSetOneProp(MapiContext context, StorePropTag propTag, object value)
		{
			base.ThrowIfNotValid(null);
			return MapiPropBagBase.InternalSetOneProp(context, base.Logon, this.StorePropertyBag, propTag, value);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004498 File Offset: 0x00002698
		protected static ErrorCode InternalSetOneProp(MapiContext context, MapiLogon logon, PropertyBag propertyBag, StorePropTag propTag, object value)
		{
			if (value is ErrorCodeValue)
			{
				ExTraceGlobals.SetPropsPropertiesTracer.TraceError<StorePropTag>(0L, "Property {0} cannot be set to error", propTag);
				return ErrorCode.CreateInvalidParameter((LID)63512U);
			}
			object obj = value;
			if (propTag.IsMultiValued && obj != null && ((Array)obj).Length == 0)
			{
				obj = null;
			}
			int countOfBlobProperties = propertyBag.CountOfBlobProperties;
			StorePropTag storePropTag;
			object obj2;
			if (countOfBlobProperties >= ConfigurationSchema.MaxNumberOfMapiProperties.Value && obj != null && !logon.AllowLargeItem() && !propertyBag.TryGetBlobProperty(context, propTag.PropId, out storePropTag, out obj2))
			{
				throw new StoreException((LID)33740U, ErrorCodeValue.TooManyProps);
			}
			return propertyBag.SetProperty(context, propTag, obj);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004540 File Offset: 0x00002740
		internal void InternalDeletePropsShouldNotFail(MapiContext context, StorePropTag[] propTags)
		{
			for (int i = 0; i < propTags.Length; i++)
			{
				this.InternalDeleteOnePropShouldNotFail(context, propTags[i]);
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004570 File Offset: 0x00002770
		internal void InternalDeleteOnePropShouldNotFail(MapiContext context, StorePropTag propTag)
		{
			base.ThrowIfNotValid(null);
			ErrorCode errorCode = this.InternalDeleteOneProp(context, propTag);
			if (errorCode != ErrorCode.NoError)
			{
				throw new ExExceptionInvalidObject((LID)64167U, string.Format("Got a problem on InternalDeleteOnePropShouldNotFail we should not have gotten ({0})", errorCode));
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000045BA File Offset: 0x000027BA
		protected virtual ErrorCode InternalDeleteOneProp(MapiContext context, StorePropTag propTag)
		{
			base.ThrowIfNotValid(null);
			if (ExTraceGlobals.DeletePropsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.DeletePropsPropertiesTracer.TraceDebug<StorePropTag>(0L, "PropTag={0}", propTag);
			}
			return this.StorePropertyBag.SetProperty(context, propTag, null);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000045F0 File Offset: 0x000027F0
		protected virtual bool TryGetPropertyImp(MapiContext context, ushort propId, out StorePropTag actualPropTag, out object propValue)
		{
			return this.StorePropertyBag.TryGetProperty(context, propId, out actualPropTag, out propValue);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004602 File Offset: 0x00002802
		protected virtual object GetPropertyValueImp(MapiContext context, StorePropTag propTag)
		{
			return this.StorePropertyBag.GetPropertyValue(context, propTag);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004614 File Offset: 0x00002814
		protected virtual void CopyToRemoveNoAccessProps(MapiContext context, MapiPropBagBase destination, List<StorePropTag> propTagsToCopy)
		{
			for (int i = propTagsToCopy.Count - 1; i >= 0; i--)
			{
				if (propTagsToCopy[i].IsCategory(9))
				{
					propTagsToCopy.RemoveAt(i);
				}
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004650 File Offset: 0x00002850
		protected void CopyToCopyPropertiesToDestination(MapiContext context, List<StorePropTag> propTagsToCopy, MapiPropBagBase destination, ref List<MapiPropertyProblem> propProblems)
		{
			Properties props = this.GetProps(context, propTagsToCopy);
			MapiPropBagBase.CopyToRemoveInvalidProps(props);
			int i = 0;
			while (i < props.Count)
			{
				StorePropTag tag = props[i].Tag;
				object value = props[i].Value;
				LargeValue largeValue = value as LargeValue;
				ErrorCode errorCode;
				if (largeValue != null)
				{
					Stream stream = null;
					Stream stream2 = null;
					try
					{
						errorCode = this.StorePropertyBag.OpenPropertyReadStream(context, tag, out stream);
						if (errorCode == ErrorCode.NoError)
						{
							errorCode = destination.StorePropertyBag.OpenPropertyWriteStream(context, tag, out stream2);
							if (errorCode == ErrorCode.NoError)
							{
								TempStream.CopyStream(stream, stream2);
							}
							else
							{
								ExTraceGlobals.SetPropsPropertiesTracer.TraceError<StorePropTag>(0L, "Setting property ({0}) failed to open destination value stream.", tag);
							}
						}
						else
						{
							ExTraceGlobals.SetPropsPropertiesTracer.TraceError<StorePropTag>(0L, "Setting property ({0}) failed to open source value stream.", tag);
						}
						goto IL_E3;
					}
					finally
					{
						if (stream != null)
						{
							stream.Dispose();
						}
						if (stream2 != null)
						{
							stream2.Dispose();
						}
					}
					goto IL_D8;
				}
				goto IL_D8;
				IL_E3:
				if (errorCode != ErrorCode.NoError)
				{
					MapiPropertyProblem item = default(MapiPropertyProblem);
					item.MapiPropTag = tag;
					item.ErrorCode = errorCode;
					if (propProblems == null)
					{
						propProblems = new List<MapiPropertyProblem>();
					}
					propProblems.Add(item);
				}
				i++;
				continue;
				IL_D8:
				errorCode = destination.InternalSetOneProp(context, tag, value);
				goto IL_E3;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000047A4 File Offset: 0x000029A4
		protected void CopyToRemoveSourceProperties(MapiContext context, List<StorePropTag> propTagsToRemove, ref List<MapiPropertyProblem> propProblems)
		{
			List<MapiPropertyProblem> list = null;
			for (int i = 0; i < propTagsToRemove.Count; i++)
			{
				StorePropTag storePropTag = propTagsToRemove[i];
				ErrorCode errorCode = this.InternalDeleteOneProp(context, storePropTag);
				if (errorCode != ErrorCode.NoError)
				{
					MapiPropertyProblem item = default(MapiPropertyProblem);
					item.MapiPropTag = storePropTag;
					item.ErrorCode = errorCode;
					if (propProblems == null)
					{
						propProblems = new List<MapiPropertyProblem>();
					}
					propProblems.Add(item);
				}
			}
			if (propProblems == null)
			{
				propProblems = list;
				return;
			}
			if (list != null && list.Count != 0)
			{
				foreach (MapiPropertyProblem item2 in list)
				{
					propProblems.Add(item2);
				}
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000486C File Offset: 0x00002A6C
		protected Exception CreateCopyPropsNotSupportedException(LID lid, MapiPropBagBase destination)
		{
			string message = string.Format("CopyProperties does not support copying from object type {0} to {1}", base.GetType(), destination.GetType());
			return new ExExceptionNoSupport(lid, message);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004898 File Offset: 0x00002A98
		protected Exception CreateCopyToNotSupportedException(LID lid, MapiPropBagBase destination)
		{
			string message = string.Format("CopyTo does not support copying from object type {0} to {1}", base.GetType(), destination.GetType());
			return new ExExceptionNoSupport(lid, message);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000048C3 File Offset: 0x00002AC3
		protected virtual void CopyToInternal(MapiContext context, MapiPropBagBase destination, IList<StorePropTag> propTagsExclude, CopyToFlags flags, ref List<MapiPropertyProblem> propProblems)
		{
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000048C5 File Offset: 0x00002AC5
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiPropBagBase>(this);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000048D0 File Offset: 0x00002AD0
		protected override void InternalDispose(bool calledFromDispose)
		{
			try
			{
				if (calledFromDispose && this.subObjects != null)
				{
					for (int i = this.subObjects.Count - 1; i >= 0; i--)
					{
						MapiBase mapiBase = this.subObjects[i];
						mapiBase.Dispose();
					}
					this.subObjects = null;
				}
			}
			finally
			{
				base.InternalDispose(calledFromDispose);
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004934 File Offset: 0x00002B34
		internal void AddSubObject(MapiBase subObject)
		{
			if (this.subObjects == null)
			{
				this.subObjects = new List<MapiBase>(4);
			}
			this.subObjects.Add(subObject);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004956 File Offset: 0x00002B56
		internal void RemoveSubObject(MapiBase subObject)
		{
			if (!base.IsDisposing)
			{
				this.subObjects.Remove(subObject);
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000496D File Offset: 0x00002B6D
		internal void CheckRights(MapiContext context, FolderSecurity.ExchangeSecurityDescriptorFolderRights requestedRights, AccessCheckOperation operation, LID lid)
		{
			this.CheckRights(context, requestedRights, true, operation, lid);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000497B File Offset: 0x00002B7B
		internal virtual void CheckPropertyRights(MapiContext context, FolderSecurity.ExchangeSecurityDescriptorFolderRights requestedRights, AccessCheckOperation operation, LID lid)
		{
			this.CheckRights(context, requestedRights, operation, lid);
		}

		// Token: 0x06000096 RID: 150
		internal abstract void CheckRights(MapiContext context, FolderSecurity.ExchangeSecurityDescriptorFolderRights requestedRights, bool allRights, AccessCheckOperation operation, LID lid);

		// Token: 0x06000097 RID: 151 RVA: 0x00004988 File Offset: 0x00002B88
		internal void CommitDirtyStreams(MapiContext context)
		{
			if (this.subObjects != null)
			{
				for (int i = 0; i < this.subObjects.Count; i++)
				{
					MapiStream mapiStream = this.subObjects[i] as MapiStream;
					if (mapiStream != null && mapiStream.IsValid)
					{
						if (mapiStream.StreamSizeInvalid)
						{
							throw new ExExceptionMaxSubmissionExceeded((LID)36120U, "Exceeded the size limitation");
						}
						mapiStream.Commit(context);
					}
				}
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000049F4 File Offset: 0x00002BF4
		internal ErrorCode GetDataReader(MapiContext context, StorePropTag propTag, out Stream stream)
		{
			base.ThrowIfNotValid(null);
			return this.StorePropertyBag.OpenPropertyReadStream(context, propTag, out stream);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004A0B File Offset: 0x00002C0B
		internal ErrorCode GetDataWriter(MapiContext context, StorePropTag propTag, out Stream stream)
		{
			base.ThrowIfNotValid(null);
			return this.StorePropertyBag.OpenPropertyWriteStream(context, propTag, out stream);
		}

		// Token: 0x04000053 RID: 83
		private static Hookable<Func<List<StorePropTag>>> getPropListTestHook = Hookable<Func<List<StorePropTag>>>.Create(false, null);

		// Token: 0x04000054 RID: 84
		private static Hookable<Func<List<Property>>> getAllPropertiesTestHook = Hookable<Func<List<Property>>>.Create(false, null);

		// Token: 0x04000055 RID: 85
		private static Hookable<Func<StorePropTag, Property>> getPropTestHook = Hookable<Func<StorePropTag, Property>>.Create(false, null);

		// Token: 0x04000056 RID: 86
		private static Hookable<Func<StorePropTag, object, ErrorCode>> setPropTestHook = Hookable<Func<StorePropTag, object, ErrorCode>>.Create(false, null);

		// Token: 0x04000057 RID: 87
		private static Hookable<Func<StorePropTag, StreamFlags, MapiStream>> openStreamTestHook = Hookable<Func<StorePropTag, StreamFlags, MapiStream>>.Create(false, null);

		// Token: 0x04000058 RID: 88
		protected static Hookable<Action<List<StorePropTag>>> copyToTestHook = Hookable<Action<List<StorePropTag>>>.Create(false, null);

		// Token: 0x04000059 RID: 89
		private List<MapiBase> subObjects;

		// Token: 0x02000011 RID: 17
		protected internal enum PropOperation
		{
			// Token: 0x0400005B RID: 91
			GetProps,
			// Token: 0x0400005C RID: 92
			SetProps,
			// Token: 0x0400005D RID: 93
			DeleteProps
		}

		// Token: 0x02000012 RID: 18
		public struct PropertyReader
		{
			// Token: 0x0600009B RID: 155 RVA: 0x00004A79 File Offset: 0x00002C79
			internal PropertyReader(MapiPropBagBase propertyBag, IList<StorePropTag> propTags)
			{
				this.propertyBag = propertyBag;
				this.propTags = propTags;
				this.index = 0;
			}

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x0600009C RID: 156 RVA: 0x00004A90 File Offset: 0x00002C90
			public int PropertyCount
			{
				get
				{
					if (this.propTags == null)
					{
						return 0;
					}
					return this.propTags.Count;
				}
			}

			// Token: 0x0600009D RID: 157 RVA: 0x00004AA8 File Offset: 0x00002CA8
			public bool ReadNext(MapiContext context, out Property property)
			{
				if (this.propTags == null || this.index >= this.propTags.Count)
				{
					property = default(Property);
					return false;
				}
				StorePropTag storePropTag = this.propTags[this.index];
				ErrorCode first = this.propertyBag.CheckPropertyOperationAllowed(context, MapiPropBagBase.PropOperation.GetProps, storePropTag, null);
				if (first != ErrorCode.NoError)
				{
					if (ExTraceGlobals.GetPropsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.GetPropsPropertiesTracer.TraceDebug<StorePropTag>(0L, "GetProp blocked: tag:[{0}]", storePropTag);
					}
					property = new Property(storePropTag.ConvertToError(), LegacyHelper.BoxedErrorCodeNotFound);
				}
				else
				{
					if (MapiPropBagBase.getPropTestHook.Value != null)
					{
						property = MapiPropBagBase.getPropTestHook.Value(storePropTag);
					}
					else
					{
						property = this.propertyBag.InternalGetOneProp(context, storePropTag);
					}
					if (ExTraceGlobals.GetPropsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.GetPropsPropertiesTracer.TraceDebug<Property>(0L, "GetProp: {0}", property);
					}
				}
				this.index++;
				return true;
			}

			// Token: 0x0400005E RID: 94
			private MapiPropBagBase propertyBag;

			// Token: 0x0400005F RID: 95
			private IList<StorePropTag> propTags;

			// Token: 0x04000060 RID: 96
			private int index;
		}
	}
}
