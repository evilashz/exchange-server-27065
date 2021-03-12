using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000201 RID: 513
	[Serializable]
	internal class XsoStringProperty : XsoProperty, IStringProperty, IProperty
	{
		// Token: 0x060013F7 RID: 5111 RVA: 0x000737F8 File Offset: 0x000719F8
		public XsoStringProperty(StorePropertyDefinition propertyDef) : base(propertyDef)
		{
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00073801 File Offset: 0x00071A01
		public XsoStringProperty(StorePropertyDefinition propertyDef, PropertyType type) : base(propertyDef, type)
		{
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x0007380B File Offset: 0x00071A0B
		public XsoStringProperty(StorePropertyDefinition propertyDef, PropertyType type, params PropertyDefinition[] prefechProperties) : base(propertyDef, type, prefechProperties)
		{
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x00073818 File Offset: 0x00071A18
		public virtual string StringData
		{
			get
			{
				if (base.State == PropertyState.Stream)
				{
					Stream stream = null;
					try
					{
						stream = base.XsoItem.OpenPropertyStream(base.PropertyDef, PropertyOpenMode.ReadOnly);
						if (stream.CanSeek)
						{
							int num = (int)((stream.Length < 32768L) ? stream.Length : 32768L);
							byte[] array = new byte[num];
							for (int i = 0; i < num; i += stream.Read(array, i, num - i))
							{
							}
							char[] chars = Encoding.Unicode.GetChars(array);
							return new string(chars);
						}
					}
					catch (Exception innerException)
					{
						throw new ConversionException("Failed to open propertyStream for " + base.PropertyDef.ToString(), innerException);
					}
					finally
					{
						if (stream != null)
						{
							stream.Dispose();
						}
					}
				}
				object obj;
				try
				{
					obj = base.XsoItem.TryGetProperty(base.PropertyDef);
					if (obj is PropertyError && ((PropertyError)obj).PropertyErrorCode == PropertyErrorCode.NotEnoughMemory)
					{
						base.XsoItem.Load(new PropertyDefinition[]
						{
							base.PropertyDef
						});
						obj = base.XsoItem.TryGetProperty(base.PropertyDef);
					}
				}
				catch (PropertyErrorException ex)
				{
					if (ex.PropertyErrors[0].PropertyErrorCode == PropertyErrorCode.NotEnoughMemory)
					{
						base.XsoItem.Load(new PropertyDefinition[]
						{
							base.PropertyDef
						});
					}
					obj = base.XsoItem.TryGetProperty(base.PropertyDef);
				}
				return (string)obj;
			}
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x000739AC File Offset: 0x00071BAC
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			base.XsoItem[base.PropertyDef] = ((IStringProperty)srcProperty).StringData;
		}
	}
}
