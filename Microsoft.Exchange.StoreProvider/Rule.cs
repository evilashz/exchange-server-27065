using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Mapi
{
	// Token: 0x02000243 RID: 579
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Rule : IComparable
	{
		// Token: 0x06000A19 RID: 2585 RVA: 0x00031094 File Offset: 0x0002F294
		public int CompareTo(object obj)
		{
			Rule rule = obj as Rule;
			if (rule == null)
			{
				throw MapiExceptionHelper.ArgumentException("obj", "argument is null or it is not a Rule");
			}
			return this._ExecutionSequence - rule._ExecutionSequence;
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x000310C8 File Offset: 0x0002F2C8
		public long ID
		{
			get
			{
				return this._ID;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x000310D0 File Offset: 0x0002F2D0
		public byte[] IDx
		{
			get
			{
				return this._IDx;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x000310D8 File Offset: 0x0002F2D8
		// (set) Token: 0x06000A1D RID: 2589 RVA: 0x000310E0 File Offset: 0x0002F2E0
		public int ExecutionSequence
		{
			get
			{
				return this._ExecutionSequence;
			}
			set
			{
				this._ExecutionSequence = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x000310E9 File Offset: 0x0002F2E9
		// (set) Token: 0x06000A1F RID: 2591 RVA: 0x000310F1 File Offset: 0x0002F2F1
		public int Level
		{
			get
			{
				return this._Level;
			}
			set
			{
				this._Level = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x000310FA File Offset: 0x0002F2FA
		// (set) Token: 0x06000A21 RID: 2593 RVA: 0x00031102 File Offset: 0x0002F302
		public RuleStateFlags StateFlags
		{
			get
			{
				return this._StateFlags;
			}
			set
			{
				this._StateFlags = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x0003110B File Offset: 0x0002F30B
		// (set) Token: 0x06000A23 RID: 2595 RVA: 0x00031113 File Offset: 0x0002F313
		public uint UserFlags
		{
			get
			{
				return this._UserFlags;
			}
			set
			{
				this._UserFlags = value;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0003111C File Offset: 0x0002F31C
		// (set) Token: 0x06000A25 RID: 2597 RVA: 0x00031124 File Offset: 0x0002F324
		public Restriction Condition
		{
			get
			{
				return this._Condition;
			}
			set
			{
				this._Condition = value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x0003112D File Offset: 0x0002F32D
		// (set) Token: 0x06000A27 RID: 2599 RVA: 0x00031135 File Offset: 0x0002F335
		public RuleAction[] Actions
		{
			get
			{
				return this._Actions;
			}
			set
			{
				this._Actions = value;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x0003113E File Offset: 0x0002F33E
		// (set) Token: 0x06000A29 RID: 2601 RVA: 0x00031146 File Offset: 0x0002F346
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x0003114F File Offset: 0x0002F34F
		// (set) Token: 0x06000A2B RID: 2603 RVA: 0x00031157 File Offset: 0x0002F357
		public string Provider
		{
			get
			{
				return this._Provider;
			}
			set
			{
				this._Provider = value;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x00031160 File Offset: 0x0002F360
		// (set) Token: 0x06000A2D RID: 2605 RVA: 0x00031168 File Offset: 0x0002F368
		public byte[] ProviderData
		{
			get
			{
				return this._ProviderData;
			}
			set
			{
				this._ProviderData = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x00031171 File Offset: 0x0002F371
		// (set) Token: 0x06000A2F RID: 2607 RVA: 0x00031179 File Offset: 0x0002F379
		public bool IsExtended
		{
			get
			{
				return this._IsExtended;
			}
			set
			{
				this._IsExtended = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x00031182 File Offset: 0x0002F382
		// (set) Token: 0x06000A31 RID: 2609 RVA: 0x0003118A File Offset: 0x0002F38A
		public PropValue[] ExtraProperties
		{
			get
			{
				return this._ExtraProperties;
			}
			set
			{
				this._ExtraProperties = value;
			}
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00031193 File Offset: 0x0002F393
		public Rule()
		{
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x0003119B File Offset: 0x0002F39B
		// (set) Token: 0x06000A34 RID: 2612 RVA: 0x000311A3 File Offset: 0x0002F3A3
		public RuleOperation Operation { get; set; }

		// Token: 0x06000A35 RID: 2613 RVA: 0x000311AC File Offset: 0x0002F3AC
		internal static PropTag[] GetUnmarshalColumns()
		{
			return Rule.UnmarshalColumns;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x000311B3 File Offset: 0x0002F3B3
		internal static PropTag[] GetUnmarshalExColumns()
		{
			return Rule.UnmarshalExColumns;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x000311BC File Offset: 0x0002F3BC
		internal static Rule CreateRuleFromProperties(MapiFolder folder, Rule existingRule, ICollection<PropValue> properties)
		{
			Rule rule = existingRule ?? new Rule();
			List<PropValue> list = new List<PropValue>(properties.Count);
			foreach (PropValue item in properties)
			{
				PropTag propTag = item.PropTag;
				if (propTag <= PropTag.RuleCondition)
				{
					if (propTag <= PropTag.RuleSequence)
					{
						if (propTag != PropTag.RuleID)
						{
							if (propTag == PropTag.RuleSequence)
							{
								rule.ExecutionSequence = item.GetInt();
								continue;
							}
						}
						else
						{
							if (existingRule == null)
							{
								rule._ID = item.GetLong();
								rule._IDx = folder.MapiStore.CreateEntryId(folder.GetProp(PropTag.Fid).GetLong(), rule._ID);
								continue;
							}
							continue;
						}
					}
					else
					{
						if (propTag == PropTag.RuleState)
						{
							rule.StateFlags = (RuleStateFlags)item.GetInt();
							continue;
						}
						if (propTag == PropTag.RuleUserFlags)
						{
							rule.UserFlags = (uint)item.GetInt();
							continue;
						}
						if (propTag == PropTag.RuleCondition)
						{
							rule.Condition = (Restriction)item.Value;
							continue;
						}
					}
				}
				else if (propTag <= PropTag.RuleProvider)
				{
					if (propTag == PropTag.RuleActions)
					{
						rule.Actions = (RuleAction[])item.Value;
						continue;
					}
					if (propTag == PropTag.RuleProvider)
					{
						rule.Provider = item.GetString();
						continue;
					}
				}
				else
				{
					if (propTag == PropTag.RuleName)
					{
						rule.Name = item.GetString();
						continue;
					}
					if (propTag == PropTag.RuleLevel)
					{
						rule.Level = item.GetInt();
						continue;
					}
					if (propTag == PropTag.RuleProviderData)
					{
						rule.ProviderData = item.GetBytes();
						continue;
					}
				}
				list.Add(item);
			}
			rule.ExtraProperties = list.ToArray();
			return rule;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x000313C0 File Offset: 0x0002F5C0
		internal static bool IsPublicFolderRule(ICollection<PropValue> properties)
		{
			foreach (PropValue propValue in properties)
			{
				if (propValue.PropTag == Rule.PR_RULE_NAME)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x00031418 File Offset: 0x0002F618
		internal ICollection<PropValue> ToProperties(bool modifyExisting, bool classicFormat)
		{
			if (this._Actions == null)
			{
				this._Actions = Array<RuleAction>.Empty;
			}
			if (this._Actions.Length == 0 && (this._StateFlags & RuleStateFlags.ExitAfterExecution) == (RuleStateFlags)0)
			{
				throw MapiExceptionHelper.DataIntegrityException("Corrupt Rule - No Actions specified");
			}
			if (this._Name == null)
			{
				this._Name = string.Empty;
			}
			if (this._Provider == null || this._Provider.Length == 0)
			{
				this._Provider = string.Empty;
			}
			PropTag[] array = classicFormat ? Rule.UnmarshalColumns : Rule.UnmarshalExColumns;
			List<PropValue> list = new List<PropValue>(11);
			if (!modifyExisting)
			{
				this._ID = 0L;
				this._IDx = null;
			}
			else if (classicFormat)
			{
				list.Add(new PropValue(array[0], this._ID));
			}
			list.Add(new PropValue(array[1], this._ExecutionSequence));
			list.Add(new PropValue(array[2], this._Level));
			list.Add(new PropValue(array[3], this._Name));
			list.Add(new PropValue(array[4], this._Provider));
			if (this._ProviderData != null && this._ProviderData.Length > 0)
			{
				list.Add(new PropValue(array[5], this._ProviderData));
			}
			list.Add(new PropValue(array[6], (int)this._StateFlags));
			list.Add(new PropValue(array[7], (int)this._UserFlags));
			if (this._IsExtended)
			{
				list.Add(new PropValue(PropTag.MessageClass, "IPM.ExtendedRule.Message"));
			}
			else if (!classicFormat)
			{
				list.Add(new PropValue(PropTag.MessageClass, "IPM.Rule.Version2.Message"));
			}
			if (classicFormat)
			{
				list.Add(new PropValue(array[9], this.Condition));
				list.Add(new PropValue(array[8], this.Actions));
			}
			else if (this.ExtraProperties != null && this.ExtraProperties.Length > 0)
			{
				if (!this._IsExtended)
				{
					throw new MapiExceptionRuleFormat("ExtraProperties may not be set on non-extended rules");
				}
				for (int i = 0; i < this.ExtraProperties.Length; i++)
				{
					for (int j = 0; j < Rule.UnmarshalExColumns.Length; j++)
					{
						if (this.ExtraProperties[i].PropTag == Rule.UnmarshalExColumns[j])
						{
							throw new MapiExceptionRuleFormat("ExtraProperties may not contain standard rule properties");
						}
					}
				}
				list.AddRange(this.ExtraProperties);
			}
			return list;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0003166C File Offset: 0x0002F86C
		private static byte[] GetPropertyAsStream(MapiMessage msg, PropTag tag)
		{
			BufferPool bufferPool;
			byte[] buffer = BufferPools.GetBuffer(98304, out bufferPool);
			byte[] result;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (MapiStream mapiStream = msg.OpenStream(tag, OpenPropertyFlags.DeferredErrors))
					{
						int num;
						do
						{
							num = mapiStream.Read(buffer, 0, buffer.Length);
							if (num > 0)
							{
								memoryStream.Write(buffer, 0, num);
							}
						}
						while (num == buffer.Length);
					}
					result = memoryStream.ToArray();
				}
			}
			catch (MapiExceptionNotFound)
			{
				result = null;
			}
			finally
			{
				if (bufferPool != null && buffer != null)
				{
					bufferPool.Release(buffer);
				}
			}
			return result;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00031724 File Offset: 0x0002F924
		private bool ReadColumn<TReturn, TParam>(Rule.RuleColumn ruleColumn, PropValue[] cols, Rule.PropertyTransform<TReturn, TParam> transform, ref TReturn destination, bool classicFormat, TReturn defaultValue)
		{
			if (!cols[(int)ruleColumn].IsError())
			{
				if (!classicFormat)
				{
					PropType propType;
					if (ruleColumn < (Rule.RuleColumn)Rule.UnmarshalExColumns.Length)
					{
						propType = Rule.UnmarshalExColumns[(int)ruleColumn].ValueType();
					}
					else
					{
						propType = (defaultValue as PropValue?).Value.PropTag.ValueType();
					}
					PropType propType2 = propType;
					if (propType2 != PropType.String)
					{
						if (propType2 == PropType.Binary)
						{
							if (cols[(int)ruleColumn].GetBytes().Length == 510)
							{
								return true;
							}
						}
					}
					else if (cols[(int)ruleColumn].GetString().Length == 255)
					{
						return true;
					}
				}
				if (transform != null)
				{
					destination = transform((TParam)((object)cols[(int)ruleColumn].Value));
				}
				else
				{
					destination = (TReturn)((object)cols[(int)ruleColumn].Value);
				}
				return false;
			}
			int errorValue = cols[(int)ruleColumn].GetErrorValue();
			if (!classicFormat && errorValue == -2147024882)
			{
				return true;
			}
			destination = defaultValue;
			return false;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0003186C File Offset: 0x0002FA6C
		internal Rule(PropValue[] cols, PropTag[] extraProps, MapiFolder mapiFolder, bool classicFormat)
		{
			bool[] array = new bool[Rule.UnmarshalExColumns.Length + ((extraProps != null) ? extraProps.Length : 0)];
			this.ReadColumn<uint, int>(Rule.RuleColumn.UserFlags, cols, (int param) => (uint)param, ref this._UserFlags, classicFormat, 0U);
			this.ReadColumn<int, int>(Rule.RuleColumn.Sequence, cols, null, ref this._ExecutionSequence, classicFormat, 0);
			this.ReadColumn<long, long>(Rule.RuleColumn.ShortID, cols, null, ref this._ID, classicFormat, 0L);
			this.ReadColumn<int, int>(Rule.RuleColumn.Level, cols, null, ref this._Level, classicFormat, 0);
			this.ReadColumn<RuleStateFlags, int>(Rule.RuleColumn.State, cols, null, ref this._StateFlags, classicFormat, RuleStateFlags.Error);
			array[3] = this.ReadColumn<string, string>(Rule.RuleColumn.Name, cols, null, ref this._Name, classicFormat, string.Empty);
			array[4] = this.ReadColumn<string, string>(Rule.RuleColumn.Provider, cols, null, ref this._Provider, classicFormat, string.Empty);
			array[5] = this.ReadColumn<byte[], byte[]>(Rule.RuleColumn.ProviderData, cols, null, ref this._ProviderData, classicFormat, null);
			if (classicFormat)
			{
				this.ReadColumn<RuleAction[], RuleAction[]>(Rule.RuleColumn.Actions, cols, null, ref this._Actions, classicFormat, Array<RuleAction>.Empty);
				this.ReadColumn<Restriction, Restriction>(Rule.RuleColumn.Condition, cols, null, ref this._Condition, classicFormat, null);
			}
			if (!classicFormat)
			{
				this.ReadColumn<bool, string>(Rule.RuleColumn.MessageClass, cols, (string param) => string.Compare(param, "IPM.ExtendedRule.Message", StringComparison.OrdinalIgnoreCase) == 0, ref this._IsExtended, classicFormat, !classicFormat);
				this.ReadColumn<byte[], byte[]>(Rule.RuleColumn.LongID, cols, null, ref this._IDx, classicFormat, null);
				array[8] = this.ReadColumn<RuleAction[], byte[]>(Rule.RuleColumn.Actions, cols, new Rule.PropertyTransform<RuleAction[], byte[]>(mapiFolder.DeserializeActions), ref this._Actions, classicFormat, Array<RuleAction>.Empty);
				array[9] = this.ReadColumn<Restriction, byte[]>(Rule.RuleColumn.Condition, cols, new Rule.PropertyTransform<Restriction, byte[]>(mapiFolder.DeserializeRestriction), ref this._Condition, classicFormat, null);
				if (extraProps != null && extraProps.Length > 0 && this._IsExtended)
				{
					this.ExtraProperties = new PropValue[extraProps.Length];
					int i;
					for (i = 0; i < extraProps.Length; i++)
					{
						array[i + Rule.UnmarshalExColumns.Length] = this.ReadColumn<PropValue, object>(i + (Rule.RuleColumn)Rule.UnmarshalExColumns.Length, cols, (object o) => new PropValue(extraProps[i], o), ref this.ExtraProperties[i], false, cols[i + Rule.UnmarshalExColumns.Length]);
					}
				}
				else
				{
					this.ExtraProperties = Array<PropValue>.Empty;
				}
				bool flag = false;
				for (int l = 0; l < array.Length; l++)
				{
					if (array[l])
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					using (MapiMessage mapiMessage = (MapiMessage)mapiFolder.OpenEntry(this._IDx, OpenEntryFlags.DeferredErrors))
					{
						for (int j = 0; j < Rule.UnmarshalExColumns.Length; j++)
						{
							if (array[j])
							{
								byte[] propertyAsStream = Rule.GetPropertyAsStream(mapiMessage, Rule.UnmarshalExColumns[j]);
								switch (j)
								{
								case 3:
									this._Name = Encoding.Unicode.GetString(propertyAsStream, 0, propertyAsStream.Length);
									break;
								case 4:
									this._Provider = Encoding.Unicode.GetString(propertyAsStream, 0, propertyAsStream.Length);
									break;
								case 5:
									this._ProviderData = propertyAsStream;
									break;
								case 8:
									this._Actions = mapiFolder.DeserializeActions(propertyAsStream);
									break;
								case 9:
									this._Condition = mapiFolder.DeserializeRestriction(propertyAsStream);
									break;
								}
							}
						}
						if (extraProps != null)
						{
							for (int k = 0; k < extraProps.Length; k++)
							{
								if (array[k + Rule.UnmarshalExColumns.Length])
								{
									byte[] propertyAsStream2 = Rule.GetPropertyAsStream(mapiMessage, extraProps[k]);
									PropType propType = extraProps[k].ValueType();
									if (propType != PropType.String)
									{
										if (propType != PropType.Binary)
										{
											throw new NotSupportedException();
										}
										this.ExtraProperties[k] = new PropValue(extraProps[k], propertyAsStream2);
									}
									else
									{
										this.ExtraProperties[k] = new PropValue(extraProps[k], Encoding.Unicode.GetString(propertyAsStream2, 0, propertyAsStream2.Length));
									}
								}
							}
						}
					}
				}
			}
			if (this._IsExtended)
			{
				this.ScrubMoveCopyActions();
			}
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00031D14 File Offset: 0x0002FF14
		private void ScrubMoveCopyActions()
		{
			for (int i = 0; i < this._Actions.Length; i++)
			{
				switch (this._Actions[i].ActionType)
				{
				case RuleAction.Type.OP_MOVE:
				{
					byte[] folderEntryID = ((RuleAction.MoveCopy)this._Actions[i]).FolderEntryID;
					uint userFlags = this._Actions[i].UserFlags;
					this._Actions[i] = new RuleAction.InMailboxMove(folderEntryID);
					this._Actions[i].UserFlags = userFlags;
					break;
				}
				case RuleAction.Type.OP_COPY:
				{
					byte[] folderEntryID = ((RuleAction.MoveCopy)this._Actions[i]).FolderEntryID;
					uint userFlags = this._Actions[i].UserFlags;
					this._Actions[i] = new RuleAction.InMailboxCopy(folderEntryID);
					this._Actions[i].UserFlags = userFlags;
					break;
				}
				}
			}
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00031DD6 File Offset: 0x0002FFD6
		private static void SerializeNullableBlob(BinarySerializer serializer, byte[] value)
		{
			serializer.Write((value != null) ? 1 : 0);
			if (value != null)
			{
				serializer.Write(value);
			}
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00031DEF File Offset: 0x0002FFEF
		private static void SerializeNullableString(BinarySerializer serializer, string value)
		{
			serializer.Write((value != null) ? 1 : 0);
			if (value != null)
			{
				serializer.Write(value);
			}
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00031E08 File Offset: 0x00030008
		private static void SerializeNullablePropValues(BinarySerializer serializer, PropValue[] values)
		{
			serializer.Write((values != null) ? 1 : 0);
			if (values != null)
			{
				serializer.Write(values);
			}
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00031E24 File Offset: 0x00030024
		private static byte[] DeserializeNullableBlob(BinaryDeserializer deserializer)
		{
			if (deserializer.ReadInt() == 0)
			{
				return null;
			}
			return deserializer.ReadBytes();
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00031E44 File Offset: 0x00030044
		private static string DeserializeNullableString(BinaryDeserializer deserializer)
		{
			if (deserializer.ReadInt() == 0)
			{
				return null;
			}
			return deserializer.ReadString();
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00031E64 File Offset: 0x00030064
		private static PropValue[] DeserializeNullablePropValues(BinaryDeserializer deserializer)
		{
			if (deserializer.ReadInt() == 0)
			{
				return null;
			}
			return deserializer.ReadPropValues();
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00031E84 File Offset: 0x00030084
		internal void SerializeRule(BinarySerializer serializer, MapiFolder folder)
		{
			Rule.SerializeNullableBlob(serializer, this._IDx);
			serializer.Write((ulong)this._ID);
			serializer.Write(this._ExecutionSequence);
			serializer.Write(this._Level);
			serializer.Write((int)this._StateFlags);
			serializer.Write((int)this._UserFlags);
			Rule.SerializeNullableBlob(serializer, folder.SerializeRestriction(this._Condition));
			Rule.SerializeNullableBlob(serializer, folder.SerializeActions(this._Actions));
			Rule.SerializeNullableString(serializer, this._Name);
			Rule.SerializeNullableString(serializer, this._Provider);
			Rule.SerializeNullableBlob(serializer, this._ProviderData);
			serializer.Write(this._IsExtended ? 1 : 0);
			Rule.SerializeNullablePropValues(serializer, this._ExtraProperties);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00031F40 File Offset: 0x00030140
		internal static Rule DeserializeRule(BinaryDeserializer deserializer, MapiFolder folder)
		{
			return new Rule
			{
				_IDx = Rule.DeserializeNullableBlob(deserializer),
				_ID = (long)deserializer.ReadUInt64(),
				_ExecutionSequence = deserializer.ReadInt(),
				_Level = deserializer.ReadInt(),
				_StateFlags = (RuleStateFlags)deserializer.ReadInt(),
				_UserFlags = (uint)deserializer.ReadInt(),
				_Condition = folder.DeserializeRestriction(Rule.DeserializeNullableBlob(deserializer)),
				_Actions = folder.DeserializeActions(Rule.DeserializeNullableBlob(deserializer)),
				_Name = Rule.DeserializeNullableString(deserializer),
				_Provider = Rule.DeserializeNullableString(deserializer),
				_ProviderData = Rule.DeserializeNullableBlob(deserializer),
				_IsExtended = (deserializer.ReadInt() != 0),
				_ExtraProperties = Rule.DeserializeNullablePropValues(deserializer)
			};
		}

		// Token: 0x04000FF6 RID: 4086
		internal const string ClassicRuleMessageClass = "IPM.Rule.Message";

		// Token: 0x04000FF7 RID: 4087
		internal const string MiddleTierRuleMessageClass = "IPM.Rule.Version2.Message";

		// Token: 0x04000FF8 RID: 4088
		internal const string ExtendedRuleMessageClass = "IPM.ExtendedRule.Message";

		// Token: 0x04000FF9 RID: 4089
		private const int pidSpecialMin = 26224;

		// Token: 0x04000FFA RID: 4090
		private const int pidStoreNonTransMin = 3648;

		// Token: 0x04000FFB RID: 4091
		private const int pidExchangeNonXmitReservedMin = 26080;

		// Token: 0x04000FFC RID: 4092
		internal static Restriction ClassicRule = new Restriction.ContentRestriction(PropTag.MessageClass, "IPM.Rule.Message", ContentFlags.IgnoreCase);

		// Token: 0x04000FFD RID: 4093
		internal static Restriction MiddleTierRule = new Restriction.ContentRestriction(PropTag.MessageClass, "IPM.Rule.Version2.Message", ContentFlags.IgnoreCase);

		// Token: 0x04000FFE RID: 4094
		internal static Restriction ExtendedRule = new Restriction.ContentRestriction(PropTag.MessageClass, "IPM.ExtendedRule.Message", ContentFlags.IgnoreCase);

		// Token: 0x04000FFF RID: 4095
		internal static Restriction NewRuleMessages = Restriction.Or(new Restriction[]
		{
			Rule.MiddleTierRule,
			Rule.ExtendedRule
		});

		// Token: 0x04001000 RID: 4096
		internal static Restriction AllRuleMessages = Restriction.Or(new Restriction[]
		{
			Rule.MiddleTierRule,
			Rule.ExtendedRule,
			Rule.ClassicRule
		});

		// Token: 0x04001001 RID: 4097
		internal byte[] _IDx;

		// Token: 0x04001002 RID: 4098
		internal long _ID;

		// Token: 0x04001003 RID: 4099
		private int _ExecutionSequence;

		// Token: 0x04001004 RID: 4100
		private int _Level;

		// Token: 0x04001005 RID: 4101
		private RuleStateFlags _StateFlags;

		// Token: 0x04001006 RID: 4102
		private uint _UserFlags;

		// Token: 0x04001007 RID: 4103
		private Restriction _Condition;

		// Token: 0x04001008 RID: 4104
		private RuleAction[] _Actions;

		// Token: 0x04001009 RID: 4105
		private string _Name;

		// Token: 0x0400100A RID: 4106
		private string _Provider;

		// Token: 0x0400100B RID: 4107
		private byte[] _ProviderData;

		// Token: 0x0400100C RID: 4108
		private bool _IsExtended;

		// Token: 0x0400100D RID: 4109
		private PropValue[] _ExtraProperties;

		// Token: 0x0400100E RID: 4110
		internal static readonly PropTag PR_RULE_ID = PropTagHelper.PropTagFromIdAndType(26228, PropType.Long);

		// Token: 0x0400100F RID: 4111
		internal static readonly PropTag PR_RULE_SEQUENCE = PropTagHelper.PropTagFromIdAndType(26230, PropType.Int);

		// Token: 0x04001010 RID: 4112
		internal static readonly PropTag PR_RULE_STATE = PropTagHelper.PropTagFromIdAndType(26231, PropType.Int);

		// Token: 0x04001011 RID: 4113
		internal static readonly PropTag PR_RULE_USER_FLAGS = PropTagHelper.PropTagFromIdAndType(26232, PropType.Int);

		// Token: 0x04001012 RID: 4114
		internal static readonly PropTag PR_RULE_CONDITION = PropTagHelper.PropTagFromIdAndType(26233, PropType.Restriction);

		// Token: 0x04001013 RID: 4115
		internal static readonly PropTag PR_RULE_ACTIONS = PropTagHelper.PropTagFromIdAndType(26240, PropType.Actions);

		// Token: 0x04001014 RID: 4116
		internal static readonly PropTag PR_RULE_PROVIDER = PropTagHelper.PropTagFromIdAndType(26241, PropType.String);

		// Token: 0x04001015 RID: 4117
		internal static readonly PropTag PR_RULE_NAME = PropTagHelper.PropTagFromIdAndType(26242, PropType.String);

		// Token: 0x04001016 RID: 4118
		internal static readonly PropTag PR_RULE_LEVEL = PropTagHelper.PropTagFromIdAndType(26243, PropType.Int);

		// Token: 0x04001017 RID: 4119
		internal static readonly PropTag PR_RULE_PROVIDER_DATA = PropTagHelper.PropTagFromIdAndType(26244, PropType.Binary);

		// Token: 0x04001018 RID: 4120
		public static readonly PropTag PR_EX_RULE_ACTIONS = PropTagHelper.PropTagFromIdAndType(3737, PropType.Binary);

		// Token: 0x04001019 RID: 4121
		internal static readonly PropTag PR_EX_RULE_CONDITION = PropTagHelper.PropTagFromIdAndType(3738, PropType.Binary);

		// Token: 0x0400101A RID: 4122
		internal static readonly PropTag PR_EX_RULE_ID = PropTag.Mid;

		// Token: 0x0400101B RID: 4123
		internal static readonly PropTag PR_EX_RULE_IDx = PropTag.EntryId;

		// Token: 0x0400101C RID: 4124
		internal static readonly PropTag PR_EX_RULE_SEQUENCE = PropTagHelper.PropTagFromIdAndType(26099, PropType.Int);

		// Token: 0x0400101D RID: 4125
		internal static readonly PropTag PR_EX_RULE_STATE = PropTagHelper.PropTagFromIdAndType(26089, PropType.Int);

		// Token: 0x0400101E RID: 4126
		internal static readonly PropTag PR_EX_RULE_USER_FLAGS = PropTagHelper.PropTagFromIdAndType(26090, PropType.Int);

		// Token: 0x0400101F RID: 4127
		internal static readonly PropTag PR_EX_RULE_PROVIDER = PropTagHelper.PropTagFromIdAndType(26091, PropType.String);

		// Token: 0x04001020 RID: 4128
		internal static readonly PropTag PR_EX_RULE_NAME = PropTagHelper.PropTagFromIdAndType(26092, PropType.String);

		// Token: 0x04001021 RID: 4129
		internal static readonly PropTag PR_EX_RULE_LEVEL = PropTagHelper.PropTagFromIdAndType(26093, PropType.Int);

		// Token: 0x04001022 RID: 4130
		internal static readonly PropTag PR_EX_RULE_PROVIDER_DATA = PropTagHelper.PropTagFromIdAndType(26094, PropType.Binary);

		// Token: 0x04001023 RID: 4131
		private static readonly PropTag[] UnmarshalColumns = new PropTag[]
		{
			Rule.PR_RULE_ID,
			Rule.PR_RULE_SEQUENCE,
			Rule.PR_RULE_LEVEL,
			Rule.PR_RULE_NAME,
			Rule.PR_RULE_PROVIDER,
			Rule.PR_RULE_PROVIDER_DATA,
			Rule.PR_RULE_STATE,
			Rule.PR_RULE_USER_FLAGS,
			Rule.PR_RULE_ACTIONS,
			Rule.PR_RULE_CONDITION
		};

		// Token: 0x04001024 RID: 4132
		internal static readonly PropTag[] UnmarshalExColumns = new PropTag[]
		{
			Rule.PR_EX_RULE_ID,
			Rule.PR_EX_RULE_SEQUENCE,
			Rule.PR_EX_RULE_LEVEL,
			Rule.PR_EX_RULE_NAME,
			Rule.PR_EX_RULE_PROVIDER,
			Rule.PR_EX_RULE_PROVIDER_DATA,
			Rule.PR_EX_RULE_STATE,
			Rule.PR_EX_RULE_USER_FLAGS,
			Rule.PR_EX_RULE_ACTIONS,
			Rule.PR_EX_RULE_CONDITION,
			Rule.PR_EX_RULE_IDx,
			PropTag.MessageClass
		};

		// Token: 0x04001025 RID: 4133
		internal static readonly ICollection<PropTag> RuleMsgPreDeleteProps = new List<PropTag>(new PropTag[]
		{
			Rule.PR_EX_RULE_CONDITION,
			Rule.PR_EX_RULE_ACTIONS,
			Rule.PR_EX_RULE_PROVIDER_DATA
		});

		// Token: 0x04001026 RID: 4134
		internal static readonly PropTag[] ExRuleGetProps = new PropTag[]
		{
			Rule.PR_EX_RULE_CONDITION,
			Rule.PR_EX_RULE_ACTIONS
		};

		// Token: 0x02000244 RID: 580
		internal enum RuleColumn
		{
			// Token: 0x0400102B RID: 4139
			ShortID,
			// Token: 0x0400102C RID: 4140
			Sequence,
			// Token: 0x0400102D RID: 4141
			Level,
			// Token: 0x0400102E RID: 4142
			Name,
			// Token: 0x0400102F RID: 4143
			Provider,
			// Token: 0x04001030 RID: 4144
			ProviderData,
			// Token: 0x04001031 RID: 4145
			State,
			// Token: 0x04001032 RID: 4146
			UserFlags,
			// Token: 0x04001033 RID: 4147
			Actions,
			// Token: 0x04001034 RID: 4148
			Condition,
			// Token: 0x04001035 RID: 4149
			LongID,
			// Token: 0x04001036 RID: 4150
			MessageClass
		}

		// Token: 0x02000245 RID: 581
		// (Invoke) Token: 0x06000A4A RID: 2634
		private delegate TReturn PropertyTransform<TReturn, TParam>(TParam propValue);
	}
}
