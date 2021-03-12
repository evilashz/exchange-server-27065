using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000009 RID: 9
	internal class BodyHelper
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000027C0 File Offset: 0x000009C0
		public BodyHelper(ICoreItem coreItem, ICorePropertyBag corePropertyBag, Encoding string8Encoding, Func<BodyReadConfiguration, Stream> getBodyConversionStreamCallback)
		{
			this.coreItem = coreItem;
			this.corePropertyBag = corePropertyBag;
			this.string8Encoding = string8Encoding;
			this.getBodyConversionStreamCallback = getBodyConversionStreamCallback;
			this.bodyState = BodyHelper.BodyState.None;
			this.alternateBodyState = BodyHelper.BodyState.None;
			this.IsOpeningStream = false;
			this.isRtfInSync = corePropertyBag.GetValueAsNullable<bool>(BodyHelper.RtfInSync);
			this.includeTheBodyPropertyBeingOpeningWhenEvaluatingIfAnyBodyPropertyIsDirty = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).RpcClientAccess.IncludeTheBodyPropertyBeingOpeningWhenEvaluatingIfAnyBodyPropertyIsDirty.Enabled;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000283A File Offset: 0x00000A3A
		public bool IsBodyChanged
		{
			get
			{
				return this.isBodyChanged;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002844 File Offset: 0x00000A44
		public static bool IsBodyProperty(PropertyDefinition propertyDefinition)
		{
			foreach (StorePropertyDefinition storePropertyDefinition in BodyHelper.bodyProperties)
			{
				if (storePropertyDefinition.Equals(propertyDefinition))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000287C File Offset: 0x00000A7C
		public static bool ContainsBodyProperty(PropertyTag[] propertyTags)
		{
			if (propertyTags != null)
			{
				foreach (PropertyTag propertyTag in propertyTags)
				{
					if (propertyTag.IsBodyProperty())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000028B8 File Offset: 0x00000AB8
		public static void RemoveBodyProperties(ref PropertyTag[] propertyTags)
		{
			if (propertyTags != null && BodyHelper.ContainsBodyProperty(propertyTags))
			{
				List<PropertyTag> list = new List<PropertyTag>(propertyTags.Length);
				foreach (PropertyTag propertyTag in propertyTags)
				{
					if (!propertyTag.IsBodyProperty())
					{
						list.Add(propertyTag);
					}
				}
				propertyTags = ((list.Count > 0) ? list.ToArray() : Array<PropertyTag>.Empty);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002921 File Offset: 0x00000B21
		public static bool IsFixupNeeded(PropertyTag propertyTag)
		{
			return propertyTag.IsBodyProperty() || propertyTag.PropertyId.IsRtfInSync();
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002939 File Offset: 0x00000B39
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002941 File Offset: 0x00000B41
		public bool IsOpeningStream { get; private set; }

		// Token: 0x0600002C RID: 44 RVA: 0x0000294C File Offset: 0x00000B4C
		public static StorePropertyDefinition GetBodyPropertyDefinition(PropertyId propertyId)
		{
			if (propertyId == PropertyId.Body)
			{
				return BodyHelper.TextBody;
			}
			if (propertyId == PropertyId.RtfCompressed)
			{
				return BodyHelper.RtfCompressed;
			}
			if (propertyId != PropertyId.Html)
			{
				throw new InvalidOperationException("Property is not a body property.");
			}
			return BodyHelper.HtmlBody;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002994 File Offset: 0x00000B94
		public bool IsNativeBodyProperty(PropertyTag propertyTag)
		{
			if (!propertyTag.IsBodyProperty())
			{
				return false;
			}
			switch (this.GetCurrentNativeBody())
			{
			case NativeBodyInfo.PlainTextBody:
				return propertyTag.PropertyId == PropertyId.Body;
			case NativeBodyInfo.RtfCompressedBody:
				return propertyTag.PropertyId == PropertyId.RtfCompressed;
			case NativeBodyInfo.HtmlBody:
				return propertyTag.PropertyId == PropertyId.Html;
			default:
				return false;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000029F8 File Offset: 0x00000BF8
		public void SetProperty(PropertyDefinition propertyDefinition, object value)
		{
			if (!this.CanWrite(propertyDefinition))
			{
				return;
			}
			if (propertyDefinition == BodyHelper.RtfInSync && value is bool)
			{
				this.isRtfInSync = new bool?((bool)value);
			}
			this.OnBeforeWrite(ref propertyDefinition);
			this.corePropertyBag.SetProperty(propertyDefinition, value);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002A48 File Offset: 0x00000C48
		public void DeleteProperty(PropertyDefinition propertyDefinition)
		{
			if (BodyHelper.IsBodyProperty(propertyDefinition) || propertyDefinition == BodyHelper.RtfInSync)
			{
				this.bodyState = BodyHelper.BodyState.None;
				this.isBodyChanged = true;
			}
			if ((propertyDefinition == BodyHelper.RtfCompressed && this.alternateBodyState == BodyHelper.BodyState.RtfCompressed) || (propertyDefinition == BodyHelper.HtmlBody && this.alternateBodyState == BodyHelper.BodyState.Html))
			{
				propertyDefinition = BodyHelper.AlternateBody;
				this.alternateBodyState = BodyHelper.BodyState.None;
			}
			this.corePropertyBag.Delete(propertyDefinition);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public Stream OpenStream(PropertyDefinition propertyDefinition, PropertyOpenMode openMode)
		{
			if (openMode == PropertyOpenMode.Create)
			{
				if (!this.CanWrite(propertyDefinition))
				{
					return Stream.Null;
				}
			}
			else if (openMode == PropertyOpenMode.Modify && !this.CanWrite(propertyDefinition))
			{
				openMode = PropertyOpenMode.ReadOnly;
			}
			if (this.coreItem.IsDirty && BodyHelper.IsBodyProperty(propertyDefinition) && this.IsAnyBodyPropertyDirty(propertyDefinition))
			{
				this.IsOpeningStream = true;
				try
				{
					this.coreItem.Flush(SaveMode.FailOnAnyConflict);
					this.corePropertyBag.Load(CoreObjectSchema.AllPropertiesOnStore);
				}
				finally
				{
					this.IsOpeningStream = false;
				}
				this.coreItem.Body.ResetBodyFormat();
			}
			if (openMode != PropertyOpenMode.ReadOnly)
			{
				this.OnBeforeWrite(ref propertyDefinition);
			}
			else if ((propertyDefinition == BodyHelper.RtfCompressed && this.alternateBodyState == BodyHelper.BodyState.RtfCompressed) || (propertyDefinition == BodyHelper.HtmlBody && this.alternateBodyState == BodyHelper.BodyState.Html))
			{
				propertyDefinition = BodyHelper.AlternateBody;
			}
			return this.corePropertyBag.OpenPropertyStream(propertyDefinition, openMode);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002B90 File Offset: 0x00000D90
		public object TryGetProperty(PropertyDefinition propertyDefinition)
		{
			if ((propertyDefinition == BodyHelper.RtfCompressed && this.alternateBodyState == BodyHelper.BodyState.RtfCompressed) || (propertyDefinition == BodyHelper.HtmlBody && this.alternateBodyState == BodyHelper.BodyState.Html))
			{
				propertyDefinition = BodyHelper.AlternateBody;
			}
			if (propertyDefinition == CoreItemSchema.NativeBodyInfo)
			{
				return (int)this.GetCurrentNativeBody();
			}
			return this.corePropertyBag.TryGetProperty(propertyDefinition);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002BE8 File Offset: 0x00000DE8
		public void OnBeforeWrite(PropertyTag propertyTag)
		{
			if (propertyTag.IsHtml())
			{
				this.bodyState = BodyHelper.BodyState.Html;
				this.isBodyChanged = true;
				return;
			}
			if (propertyTag.IsRtfCompressed())
			{
				if (this.bodyState == BodyHelper.BodyState.None || this.bodyState == BodyHelper.BodyState.PlainText)
				{
					this.bodyState = BodyHelper.BodyState.RtfCompressed;
					this.isBodyChanged = true;
					return;
				}
			}
			else if (propertyTag.IsBody() && this.bodyState == BodyHelper.BodyState.None)
			{
				this.bodyState = BodyHelper.BodyState.PlainText;
				this.isBodyChanged = true;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002C52 File Offset: 0x00000E52
		public void Reset()
		{
			this.bodyState = BodyHelper.BodyState.None;
			this.isBodyChanged = false;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002C62 File Offset: 0x00000E62
		public void InitiatePropertyEvaluation()
		{
			this.nativeBodyInfo = this.GetCurrentNativeBody();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002C70 File Offset: 0x00000E70
		public void FixupProperty(ref PropertyValue propertyValue, FixupMapping mapping)
		{
			BodyHelper.BodyPropertyMapper.Fixup(ref propertyValue, this.nativeBodyInfo, mapping);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002C80 File Offset: 0x00000E80
		public void FixupProperties(PropertyValue[] values, FixupMapping mapping)
		{
			this.InitiatePropertyEvaluation();
			int num;
			if (BodyHelper.IsSingleBodyPropertyRequested(values, out num))
			{
				if (this.DoesABodyPropertyExistOnMessage())
				{
					if (!this.IsNativeBodyProperty(values[num].PropertyTag))
					{
						values[num] = new PropertyValue(new PropertyTag(values[num].PropertyTag.PropertyId, PropertyType.Error), (ErrorCode)2147942414U);
					}
					bool flag = values[num].PropertyTag.PropertyId == PropertyId.RtfCompressed;
					for (int i = 0; i < values.Length; i++)
					{
						if (values[i].PropertyTag.PropertyId.IsRtfInSync())
						{
							if (flag)
							{
								values[i] = BodyHelper.True_RtfInSync;
							}
							else
							{
								values[i] = ((this.nativeBodyInfo == NativeBodyInfo.RtfCompressedBody) ? BodyHelper.True_RtfInSync : BodyHelper.False_RtfInSync);
							}
						}
					}
					return;
				}
			}
			else if (BodyHelper.IsOnlyPlainTextAndRtfPropertyRequested(values) && this.IsNativeBodyProperty(PropertyTag.Html))
			{
				if (this.DoesABodyPropertyExistOnMessage())
				{
					for (int j = 0; j < values.Length; j++)
					{
						PropertyId propertyId = values[j].PropertyTag.PropertyId;
						if (propertyId.IsRtfInSync())
						{
							values[j] = BodyHelper.True_RtfInSync;
						}
						else if (propertyId.IsBody() || propertyId.IsRtfCompressed())
						{
							values[j] = new PropertyValue(new PropertyTag(propertyId, PropertyType.Error), (ErrorCode)2147942414U);
						}
					}
					return;
				}
			}
			else
			{
				for (int k = 0; k < values.Length; k++)
				{
					this.FixupProperty(ref values[k], mapping);
				}
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002E34 File Offset: 0x00001034
		public bool IsConversionNeeded(PropertyTag propertyTag)
		{
			return propertyTag.IsBodyProperty() && !this.IsBodyPropertyOnMessage(propertyTag) && this.DoesABodyPropertyExistOnMessage() && !this.IsNativeBodyProperty(propertyTag);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002E5C File Offset: 0x0000105C
		public Stream GetConversionStream(PropertyTag propertyTag)
		{
			if (!this.IsConversionNeeded(propertyTag))
			{
				return null;
			}
			PropertyId propertyId = propertyTag.PropertyId;
			BodyFormat targetFormat;
			if (propertyId != PropertyId.Body)
			{
				if (propertyId != PropertyId.RtfCompressed)
				{
					if (propertyId != PropertyId.Html)
					{
						return null;
					}
					targetFormat = BodyFormat.TextHtml;
				}
				else
				{
					targetFormat = BodyFormat.ApplicationRtf;
				}
			}
			else
			{
				targetFormat = BodyFormat.TextPlain;
			}
			int codePage = this.string8Encoding.CodePage;
			if (propertyTag.PropertyType == PropertyType.Unicode)
			{
				codePage = Encoding.Unicode.CodePage;
			}
			BodyReadConfiguration arg = new BodyReadConfiguration(targetFormat, ConvertUtils.GetCharsetFromCodepage(codePage), true);
			return this.getBodyConversionStreamCallback(arg);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002EE0 File Offset: 0x000010E0
		public void PrepareForSave()
		{
			if ((this.alternateBodyState == BodyHelper.BodyState.Html && this.bodyState == BodyHelper.BodyState.RtfCompressed && this.isRtfInSync != true) || (this.alternateBodyState == BodyHelper.BodyState.RtfCompressed && this.bodyState == BodyHelper.BodyState.Html && this.isRtfInSync == true))
			{
				PropertyDefinition propertyDefinition = (this.alternateBodyState == BodyHelper.BodyState.Html) ? BodyHelper.HtmlBody : BodyHelper.RtfCompressed;
				using (Stream stream = this.corePropertyBag.OpenPropertyStream(BodyHelper.AlternateBody, PropertyOpenMode.ReadOnly))
				{
					using (Stream stream2 = this.corePropertyBag.OpenPropertyStream(propertyDefinition, PropertyOpenMode.Create))
					{
						Util.StreamHandler.CopyStreamData(stream, stream2, null, 0, 65536);
					}
				}
				this.bodyState = this.alternateBodyState;
				this.isBodyChanged = true;
			}
			if (this.alternateBodyState != BodyHelper.BodyState.None)
			{
				this.coreItem.PropertyBag.Delete(BodyHelper.AlternateBody);
				this.alternateBodyState = BodyHelper.BodyState.None;
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000300C File Offset: 0x0000120C
		public void UpdateBodyPreviewIfNeeded(Body body)
		{
			if (!this.IsBodyChanged)
			{
				return;
			}
			switch (this.GetCurrentNativeBody())
			{
			case NativeBodyInfo.PlainTextBody:
				this.corePropertyBag.Delete(BodyHelper.RtfCompressed);
				this.corePropertyBag.SetProperty(BodyHelper.RtfInSync, BodyHelper.RtfInSync_False);
				this.corePropertyBag.Delete(BodyHelper.HtmlBody);
				break;
			case NativeBodyInfo.RtfCompressedBody:
				this.corePropertyBag.Delete(BodyHelper.TextBody);
				this.corePropertyBag.Delete(BodyHelper.HtmlBody);
				this.corePropertyBag.SetProperty(BodyHelper.RtfInSync, BodyHelper.RtfInSync_True);
				break;
			case NativeBodyInfo.HtmlBody:
				this.corePropertyBag.Delete(BodyHelper.TextBody);
				this.corePropertyBag.Delete(BodyHelper.RtfCompressed);
				this.corePropertyBag.SetProperty(BodyHelper.RtfInSync, BodyHelper.RtfInSync_False);
				break;
			}
			body.NotifyPreviewNeedsUpdated();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000030F0 File Offset: 0x000012F0
		private static bool IsSingleBodyPropertyRequested(PropertyValue[] values, out int propertyIndex)
		{
			propertyIndex = -1;
			int num = 0;
			int num2 = 0;
			while (num2 < values.Length && num < 2)
			{
				if (values[num2].PropertyTag.IsBodyProperty())
				{
					num++;
					propertyIndex = num2;
				}
				num2++;
			}
			return num == 1;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003134 File Offset: 0x00001334
		private static bool IsOnlyPlainTextAndRtfPropertyRequested(PropertyValue[] values)
		{
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < values.Length; i++)
			{
				PropertyId propertyId = values[i].PropertyTag.PropertyId;
				if (propertyId.IsHtml())
				{
					return false;
				}
				if (propertyId.IsRtfCompressed())
				{
					flag2 = true;
				}
				else if (propertyId.IsBody())
				{
					flag = true;
				}
			}
			return flag && flag2;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003190 File Offset: 0x00001390
		private void OnBeforeWrite(ref PropertyDefinition propertyDefinition)
		{
			if (propertyDefinition != BodyHelper.HtmlBody)
			{
				if (propertyDefinition == BodyHelper.RtfCompressed)
				{
					if (this.bodyState == BodyHelper.BodyState.Html)
					{
						this.alternateBodyState = BodyHelper.BodyState.RtfCompressed;
						propertyDefinition = BodyHelper.AlternateBody;
					}
					if (this.bodyState == BodyHelper.BodyState.None || this.bodyState == BodyHelper.BodyState.PlainText)
					{
						this.bodyState = BodyHelper.BodyState.RtfCompressed;
						this.isBodyChanged = true;
						return;
					}
				}
				else if (propertyDefinition == BodyHelper.TextBody && this.bodyState == BodyHelper.BodyState.None)
				{
					this.bodyState = BodyHelper.BodyState.PlainText;
					this.isBodyChanged = true;
				}
				return;
			}
			if (this.bodyState == BodyHelper.BodyState.RtfCompressed)
			{
				this.alternateBodyState = BodyHelper.BodyState.Html;
				propertyDefinition = BodyHelper.AlternateBody;
				return;
			}
			this.bodyState = BodyHelper.BodyState.Html;
			this.isBodyChanged = true;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000322C File Offset: 0x0000142C
		private bool CanWrite(PropertyDefinition propertyDefinition)
		{
			return (propertyDefinition != BodyHelper.TextBody || (this.bodyState != BodyHelper.BodyState.Html && this.bodyState != BodyHelper.BodyState.RtfCompressed)) && propertyDefinition != BodyHelper.AlternateBody;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003258 File Offset: 0x00001458
		private bool IsBodyPropertyOnMessage(StorePropertyDefinition propertyDefinition)
		{
			object propertyValue = this.corePropertyBag.TryGetProperty(propertyDefinition);
			return !PropertyError.IsPropertyNotFound(propertyValue);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003280 File Offset: 0x00001480
		private bool IsAnyBodyPropertyDirty(PropertyDefinition propertyDefinition)
		{
			foreach (StorePropertyDefinition storePropertyDefinition in BodyHelper.bodyProperties)
			{
				if (this.corePropertyBag.IsPropertyDirty(storePropertyDefinition) && (!storePropertyDefinition.Equals(propertyDefinition) || this.includeTheBodyPropertyBeingOpeningWhenEvaluatingIfAnyBodyPropertyIsDirty))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000032CB File Offset: 0x000014CB
		private bool IsBodyPropertyOnMessage(PropertyTag propertyTag)
		{
			return this.IsBodyPropertyOnMessage(BodyHelper.GetBodyPropertyDefinition(propertyTag.PropertyId));
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000032E0 File Offset: 0x000014E0
		private bool DoesABodyPropertyExistOnMessage()
		{
			foreach (StorePropertyDefinition propertyDefinition in BodyHelper.bodyProperties)
			{
				if (this.IsBodyPropertyOnMessage(propertyDefinition))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003318 File Offset: 0x00001518
		private NativeBodyInfo GetCurrentNativeBody()
		{
			switch (this.bodyState)
			{
			case BodyHelper.BodyState.PlainText:
				return NativeBodyInfo.PlainTextBody;
			case BodyHelper.BodyState.RtfCompressed:
				if (this.alternateBodyState == BodyHelper.BodyState.Html && !this.isRtfInSync.GetValueOrDefault(false))
				{
					return NativeBodyInfo.HtmlBody;
				}
				return NativeBodyInfo.RtfCompressedBody;
			case BodyHelper.BodyState.Html:
				if (this.alternateBodyState == BodyHelper.BodyState.RtfCompressed && this.isRtfInSync.GetValueOrDefault(false))
				{
					return NativeBodyInfo.RtfCompressedBody;
				}
				return NativeBodyInfo.HtmlBody;
			default:
			{
				NativeBodyInfo valueOrDefault = this.corePropertyBag.GetValueOrDefault<NativeBodyInfo>(CoreItemSchema.NativeBodyInfo);
				if (valueOrDefault != NativeBodyInfo.Undefined)
				{
					return valueOrDefault;
				}
				if (this.IsBodyPropertyOnMessage(PropertyTag.Html))
				{
					return NativeBodyInfo.HtmlBody;
				}
				if (this.IsBodyPropertyOnMessage(PropertyTag.RtfCompressed))
				{
					return NativeBodyInfo.RtfCompressedBody;
				}
				if (this.IsBodyPropertyOnMessage(PropertyTag.Body))
				{
					return NativeBodyInfo.PlainTextBody;
				}
				return NativeBodyInfo.Undefined;
			}
			}
		}

		// Token: 0x04000013 RID: 19
		public static readonly PropertyTag[] AllBodyPropertiesUnicode = new PropertyTag[]
		{
			PropertyTag.Body,
			PropertyTag.RtfCompressed,
			PropertyTag.RtfInSync,
			PropertyTag.Html
		};

		// Token: 0x04000014 RID: 20
		public static readonly PropertyTag[] AllBodyPropertiesString8 = new PropertyTag[]
		{
			new PropertyTag(PropertyTag.Body.PropertyId, PropertyType.String8),
			PropertyTag.RtfCompressed,
			PropertyTag.RtfInSync,
			PropertyTag.Html
		};

		// Token: 0x04000015 RID: 21
		private static readonly StorePropertyDefinition TextBody = ItemSchema.TextBody;

		// Token: 0x04000016 RID: 22
		private static readonly StorePropertyDefinition HtmlBody = PropertyTagPropertyDefinition.CreateCustom("Html", PropertyTag.Html);

		// Token: 0x04000017 RID: 23
		private static readonly StorePropertyDefinition RtfCompressed = PropertyTagPropertyDefinition.CreateCustom("RtfCompressed", PropertyTag.RtfCompressed);

		// Token: 0x04000018 RID: 24
		private static readonly StorePropertyDefinition AlternateBody = PropertyTagPropertyDefinition.CreateCustom("AlternateBody", PropertyTag.AlternateBestBody);

		// Token: 0x04000019 RID: 25
		private static readonly StorePropertyDefinition RtfInSync = PropertyTagPropertyDefinition.CreateCustom("RtfInSync", PropertyTag.RtfInSync);

		// Token: 0x0400001A RID: 26
		private static readonly object RtfInSync_True = true;

		// Token: 0x0400001B RID: 27
		private static readonly object RtfInSync_False = false;

		// Token: 0x0400001C RID: 28
		private static readonly StorePropertyDefinition[] bodyProperties = new StorePropertyDefinition[]
		{
			BodyHelper.TextBody,
			BodyHelper.RtfCompressed,
			BodyHelper.HtmlBody
		};

		// Token: 0x0400001D RID: 29
		private static readonly PropertyId[] bodyPropertyIds = new PropertyId[]
		{
			PropertyId.Body,
			PropertyId.RtfCompressed,
			PropertyId.Html
		};

		// Token: 0x0400001E RID: 30
		private static readonly PropertyValue True_RtfInSync = new PropertyValue(PropertyTag.RtfInSync, true);

		// Token: 0x0400001F RID: 31
		private static readonly PropertyValue False_RtfInSync = new PropertyValue(PropertyTag.RtfInSync, false);

		// Token: 0x04000020 RID: 32
		private readonly ICoreItem coreItem;

		// Token: 0x04000021 RID: 33
		private readonly ICorePropertyBag corePropertyBag;

		// Token: 0x04000022 RID: 34
		private readonly Encoding string8Encoding;

		// Token: 0x04000023 RID: 35
		private readonly Func<BodyReadConfiguration, Stream> getBodyConversionStreamCallback;

		// Token: 0x04000024 RID: 36
		private readonly bool includeTheBodyPropertyBeingOpeningWhenEvaluatingIfAnyBodyPropertyIsDirty;

		// Token: 0x04000025 RID: 37
		private BodyHelper.BodyState bodyState;

		// Token: 0x04000026 RID: 38
		private BodyHelper.BodyState alternateBodyState;

		// Token: 0x04000027 RID: 39
		private NativeBodyInfo nativeBodyInfo;

		// Token: 0x04000028 RID: 40
		private bool isBodyChanged;

		// Token: 0x04000029 RID: 41
		private bool? isRtfInSync;

		// Token: 0x0200000A RID: 10
		private enum BodyState
		{
			// Token: 0x0400002C RID: 44
			None,
			// Token: 0x0400002D RID: 45
			PlainText,
			// Token: 0x0400002E RID: 46
			RtfCompressed,
			// Token: 0x0400002F RID: 47
			Html
		}

		// Token: 0x0200000B RID: 11
		private static class BodyPropertyMapper
		{
			// Token: 0x06000045 RID: 69 RVA: 0x0000357C File Offset: 0x0000177C
			public static void Fixup(ref PropertyValue propertyValue, NativeBodyInfo nativeBodyInfo, FixupMapping mapping)
			{
				PropertyId propertyId = propertyValue.PropertyTag.PropertyId;
				int num = 0;
				if (BodyHelper.BodyPropertyMapper.TryGetBodyIndex(propertyId, out num) && ((propertyValue.IsError && (ErrorCode)propertyValue.Value == (ErrorCode)2147746063U) || propertyId.IsRtfInSync()))
				{
					int nativeBodyIndex = BodyHelper.BodyPropertyMapper.GetNativeBodyIndex(nativeBodyInfo);
					PropertyValue? propertyValue2 = BodyHelper.BodyPropertyMapper.GetBodyPropertyMap(mapping)[num][nativeBodyIndex];
					if (propertyValue2 != null)
					{
						propertyValue = propertyValue2.Value;
					}
				}
			}

			// Token: 0x06000046 RID: 70 RVA: 0x00003600 File Offset: 0x00001800
			private static PropertyValue?[][] GetBodyPropertyMap(FixupMapping mapping)
			{
				switch (mapping)
				{
				case FixupMapping.GetProperties:
					return BodyHelper.BodyPropertyMapper.bodyFixupValues_GetProperty;
				case FixupMapping.FastTransfer:
					return BodyHelper.BodyPropertyMapper.bodyFixupValues_FastTransfer;
				case FixupMapping.FastTransferCopyProperties:
					return BodyHelper.BodyPropertyMapper.bodyFixupValues_FastTransferCopyProperties;
				default:
					throw new InvalidOperationException(string.Format("Unknown FixupMapping: {0}", mapping));
				}
			}

			// Token: 0x06000047 RID: 71 RVA: 0x0000364C File Offset: 0x0000184C
			private static bool TryGetBodyIndex(PropertyId propertyId, out int bodyIndex)
			{
				bodyIndex = 0;
				if (propertyId <= PropertyId.Body)
				{
					if (propertyId == PropertyId.RtfInSync)
					{
						bodyIndex = 3;
						return true;
					}
					if (propertyId == PropertyId.Body)
					{
						bodyIndex = 0;
						return true;
					}
				}
				else
				{
					if (propertyId == PropertyId.RtfCompressed)
					{
						bodyIndex = 1;
						return true;
					}
					if (propertyId == PropertyId.Html)
					{
						bodyIndex = 2;
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000048 RID: 72 RVA: 0x000036A0 File Offset: 0x000018A0
			private static int GetNativeBodyIndex(NativeBodyInfo nativeBodyInfo)
			{
				switch (nativeBodyInfo)
				{
				case NativeBodyInfo.PlainTextBody:
					return 0;
				case NativeBodyInfo.RtfCompressedBody:
					return 1;
				case NativeBodyInfo.HtmlBody:
					return 2;
				case NativeBodyInfo.ClearSignedBody:
					return 3;
				default:
					return 4;
				}
			}

			// Token: 0x04000030 RID: 48
			private static readonly PropertyValue True_RtfInSync = new PropertyValue(PropertyTag.RtfInSync, true);

			// Token: 0x04000031 RID: 49
			private static readonly PropertyValue False_RtfInSync = new PropertyValue(PropertyTag.RtfInSync, false);

			// Token: 0x04000032 RID: 50
			private static readonly PropertyValue NotEnoughMemory_Rtf = PropertyValue.CreateNotEnoughMemory(PropertyId.RtfCompressed);

			// Token: 0x04000033 RID: 51
			private static readonly PropertyValue NotFound_Rtf = PropertyValue.CreateNotFound(PropertyId.RtfCompressed);

			// Token: 0x04000034 RID: 52
			private static readonly PropertyValue NotEnoughMemory_Body = PropertyValue.CreateNotEnoughMemory(PropertyId.Body);

			// Token: 0x04000035 RID: 53
			private static readonly PropertyValue NotFound_Body = PropertyValue.CreateNotFound(PropertyId.Body);

			// Token: 0x04000036 RID: 54
			private static readonly PropertyValue NotEnoughMemory_Html = PropertyValue.CreateNotEnoughMemory(PropertyId.Html);

			// Token: 0x04000037 RID: 55
			private static readonly PropertyValue NotFound_Html = PropertyValue.CreateNotFound(PropertyId.Html);

			// Token: 0x04000038 RID: 56
			private static readonly PropertyValue? Existing = null;

			// Token: 0x04000039 RID: 57
			private static readonly PropertyValue?[][] bodyFixupValues_GetProperty = new PropertyValue?[][]
			{
				new PropertyValue?[]
				{
					BodyHelper.BodyPropertyMapper.Existing,
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotEnoughMemory_Body),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotFound_Body),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotFound_Body),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotFound_Body)
				},
				new PropertyValue?[]
				{
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotFound_Rtf),
					BodyHelper.BodyPropertyMapper.Existing,
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotEnoughMemory_Rtf),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotFound_Rtf),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotFound_Rtf)
				},
				new PropertyValue?[]
				{
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotFound_Html),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotFound_Html),
					BodyHelper.BodyPropertyMapper.Existing,
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotFound_Html),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotFound_Html)
				},
				new PropertyValue?[]
				{
					new PropertyValue?(BodyHelper.BodyPropertyMapper.False_RtfInSync),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.True_RtfInSync),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.False_RtfInSync),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.False_RtfInSync),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.False_RtfInSync)
				}
			};

			// Token: 0x0400003A RID: 58
			private static readonly PropertyValue?[][] bodyFixupValues_FastTransfer = new PropertyValue?[][]
			{
				new PropertyValue?[]
				{
					BodyHelper.BodyPropertyMapper.Existing,
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotEnoughMemory_Body),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotEnoughMemory_Body),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotFound_Body),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotFound_Body)
				},
				new PropertyValue?[]
				{
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotEnoughMemory_Rtf),
					BodyHelper.BodyPropertyMapper.Existing,
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotEnoughMemory_Rtf),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotFound_Rtf),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotFound_Rtf)
				},
				new PropertyValue?[]
				{
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotFound_Html),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotFound_Html),
					BodyHelper.BodyPropertyMapper.Existing,
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotFound_Html),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotFound_Html)
				},
				new PropertyValue?[]
				{
					new PropertyValue?(BodyHelper.BodyPropertyMapper.True_RtfInSync),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.True_RtfInSync),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.True_RtfInSync),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.False_RtfInSync),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.False_RtfInSync)
				}
			};

			// Token: 0x0400003B RID: 59
			private static readonly PropertyValue?[][] bodyFixupValues_FastTransferCopyProperties = new PropertyValue?[][]
			{
				new PropertyValue?[]
				{
					BodyHelper.BodyPropertyMapper.Existing,
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotEnoughMemory_Body),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotEnoughMemory_Body),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotFound_Body),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotFound_Body)
				},
				new PropertyValue?[]
				{
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotEnoughMemory_Rtf),
					BodyHelper.BodyPropertyMapper.Existing,
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotEnoughMemory_Rtf),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotFound_Rtf),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotFound_Rtf)
				},
				new PropertyValue?[]
				{
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotEnoughMemory_Html),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotEnoughMemory_Html),
					BodyHelper.BodyPropertyMapper.Existing,
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotFound_Html),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.NotFound_Html)
				},
				new PropertyValue?[]
				{
					new PropertyValue?(BodyHelper.BodyPropertyMapper.True_RtfInSync),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.True_RtfInSync),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.True_RtfInSync),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.False_RtfInSync),
					new PropertyValue?(BodyHelper.BodyPropertyMapper.False_RtfInSync)
				}
			};
		}
	}
}
