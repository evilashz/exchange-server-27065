using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Microsoft.Exchange.ManagementGUI
{
	// Token: 0x02000005 RID: 5
	[DefaultProperty("Icons")]
	public class IconLibrary : Component
	{
		// Token: 0x06000132 RID: 306 RVA: 0x00005CFC File Offset: 0x00003EFC
		static IconLibrary()
		{
			TypeDescriptor.AddAttributes(typeof(Icon), new Attribute[]
			{
				new TypeConverterAttribute(typeof(IconLibrary.GlobalIconConverter)),
				new EditorAttribute(typeof(IconLibrary.GlobalIconEditor), typeof(UITypeEditor))
			});
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005D4F File Offset: 0x00003F4F
		public IconLibrary()
		{
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005D57 File Offset: 0x00003F57
		public IconLibrary(IContainer container) : this()
		{
			container.Add(this);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005D66 File Offset: 0x00003F66
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Icons.Clear();
				if (this.smallImageList != null)
				{
					this.smallImageList.Dispose();
				}
				if (this.largeImageList != null)
				{
					this.largeImageList.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00005DA3 File Offset: 0x00003FA3
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public IconLibrary.IconReferenceCollection Icons
		{
			get
			{
				if (this.icons == null)
				{
					this.icons = new IconLibrary.IconReferenceCollection(this);
				}
				return this.icons;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00005DBF File Offset: 0x00003FBF
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		public ImageList SmallImageList
		{
			get
			{
				if (this.smallImageList == null)
				{
					this.smallImageList = this.CreateImageList(SystemInformation.SmallIconSize);
				}
				return this.smallImageList;
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005DE0 File Offset: 0x00003FE0
		private bool ShouldSerializeSmallImageList()
		{
			return false;
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00005DE3 File Offset: 0x00003FE3
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		public ImageList LargeImageList
		{
			get
			{
				if (this.largeImageList == null)
				{
					this.largeImageList = this.CreateImageList(SystemInformation.IconSize);
				}
				return this.largeImageList;
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005E04 File Offset: 0x00004004
		private bool ShouldSerializeLargeImageList()
		{
			return false;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005E08 File Offset: 0x00004008
		private ImageList CreateImageList(Size imageSize)
		{
			ImageList imageList = new ImageList();
			imageList.ColorDepth = ColorDepth.Depth32Bit;
			imageList.TransparentColor = Color.Transparent;
			imageList.ImageSize = imageSize;
			foreach (IconLibrary.IconReference iconReference in this.Icons)
			{
				imageList.Images.Add(iconReference.Name, iconReference.ToBitmap(imageSize));
			}
			return imageList;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005E88 File Offset: 0x00004088
		public Icon GetIcon(string name, int index)
		{
			Icon result = null;
			if (this.icons != null)
			{
				IconLibrary.IconReference iconReference = null;
				if (string.IsNullOrEmpty(name))
				{
					if (index >= 0 && index < this.icons.Count)
					{
						iconReference = this.icons[index];
					}
				}
				else
				{
					iconReference = this.Icons[name];
				}
				if (iconReference != null)
				{
					result = iconReference.Icon;
				}
			}
			return result;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005EE2 File Offset: 0x000040E2
		public static Bitmap ToSmallBitmap(Icon icon)
		{
			return IconLibrary.ToBitmap(icon, SystemInformation.SmallIconSize);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005EF0 File Offset: 0x000040F0
		public static Bitmap ToBitmap(Icon icon, Size size)
		{
			Bitmap bitmap = null;
			if (icon != null)
			{
				using (Icon icon2 = new Icon(icon, size))
				{
					bitmap = icon2.ToBitmap();
					if (bitmap.Width > size.Width || bitmap.Height > size.Height)
					{
						Bitmap bitmap2 = new Bitmap(bitmap, size);
						bitmap.Dispose();
						bitmap = bitmap2;
					}
				}
			}
			return bitmap;
		}

		// Token: 0x04000134 RID: 308
		private IconLibrary.IconReferenceCollection icons;

		// Token: 0x04000135 RID: 309
		private ImageList smallImageList;

		// Token: 0x04000136 RID: 310
		private ImageList largeImageList;

		// Token: 0x02000006 RID: 6
		[Editor(typeof(IconLibrary.IconReferenceEditor), typeof(UITypeEditor))]
		[TypeConverter(typeof(IconLibrary.IconReferenceConverter))]
		[ImmutableObject(true)]
		public class IconReference
		{
			// Token: 0x0600013F RID: 319 RVA: 0x00005F5C File Offset: 0x0000415C
			public IconReference(string name, Icon icon)
			{
				if (string.IsNullOrEmpty(name))
				{
					throw new ArgumentNullException("name");
				}
				if (icon == null)
				{
					throw new ArgumentNullException("icon");
				}
				this.name = (name ?? "");
				this.icon = icon;
			}

			// Token: 0x1700012C RID: 300
			// (get) Token: 0x06000140 RID: 320 RVA: 0x00005F9C File Offset: 0x0000419C
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x06000141 RID: 321 RVA: 0x00005FA4 File Offset: 0x000041A4
			[TypeConverter(typeof(IconLibrary.GlobalIconConverter))]
			[Editor(typeof(IconLibrary.GlobalIconEditor), typeof(UITypeEditor))]
			public Icon Icon
			{
				get
				{
					return this.icon;
				}
			}

			// Token: 0x06000142 RID: 322 RVA: 0x00005FAC File Offset: 0x000041AC
			public Bitmap ToBitmap(Size size)
			{
				return IconLibrary.ToBitmap(this.Icon, size);
			}

			// Token: 0x04000137 RID: 311
			private string name;

			// Token: 0x04000138 RID: 312
			private Icon icon;
		}

		// Token: 0x02000007 RID: 7
		[Editor(typeof(IconLibrary.IconReferenceCollectionEditor), typeof(UITypeEditor))]
		public class IconReferenceCollection : Collection<IconLibrary.IconReference>
		{
			// Token: 0x06000143 RID: 323 RVA: 0x00005FBA File Offset: 0x000041BA
			internal IconReferenceCollection(IconLibrary owner)
			{
				this.owner = owner;
			}

			// Token: 0x06000144 RID: 324 RVA: 0x00005FCC File Offset: 0x000041CC
			public void AddRange(IconLibrary.IconReference[] iconReferences)
			{
				if (iconReferences == null)
				{
					throw new ArgumentNullException("iconReferences");
				}
				for (int i = 0; i < iconReferences.Length; i++)
				{
					base.Add(iconReferences[i]);
				}
			}

			// Token: 0x06000145 RID: 325 RVA: 0x00005FFE File Offset: 0x000041FE
			public void Add(Enum name, Icon icon)
			{
				base.Add(new IconLibrary.IconReference(name.ToString(), icon));
			}

			// Token: 0x06000146 RID: 326 RVA: 0x00006012 File Offset: 0x00004212
			public void Add(string name, Icon icon)
			{
				base.Add(new IconLibrary.IconReference(name, icon));
			}

			// Token: 0x06000147 RID: 327 RVA: 0x00006021 File Offset: 0x00004221
			protected override void ClearItems()
			{
				IconLibrary.IconReferenceCollection.ClearImageList(this.owner.smallImageList);
				IconLibrary.IconReferenceCollection.ClearImageList(this.owner.largeImageList);
				base.ClearItems();
			}

			// Token: 0x06000148 RID: 328 RVA: 0x0000604C File Offset: 0x0000424C
			private static void ClearImageList(ImageList imageList)
			{
				if (imageList != null)
				{
					foreach (object obj in imageList.Images)
					{
						Image image = (Image)obj;
						image.Dispose();
					}
					imageList.Images.Clear();
				}
			}

			// Token: 0x1700012E RID: 302
			public IconLibrary.IconReference this[string name]
			{
				get
				{
					int num = this.IndexOf(name);
					if (-1 == num)
					{
						return null;
					}
					return base[num];
				}
			}

			// Token: 0x0600014A RID: 330 RVA: 0x000060D8 File Offset: 0x000042D8
			public int IndexOf(string name)
			{
				if (!string.IsNullOrEmpty(name))
				{
					for (int i = 0; i < base.Count; i++)
					{
						if (StringComparer.InvariantCultureIgnoreCase.Compare(base[i].Name, name) == 0)
						{
							return i;
						}
					}
				}
				return -1;
			}

			// Token: 0x1700012F RID: 303
			public IconLibrary.IconReference this[Icon icon]
			{
				get
				{
					int num = this.IndexOf(icon);
					if (-1 == num)
					{
						return null;
					}
					return base[num];
				}
			}

			// Token: 0x0600014C RID: 332 RVA: 0x00006140 File Offset: 0x00004340
			public int IndexOf(Icon icon)
			{
				if (icon != null)
				{
					for (int i = 0; i < base.Count; i++)
					{
						if (base[i].Icon == icon)
						{
							return i;
						}
					}
				}
				return -1;
			}

			// Token: 0x0600014D RID: 333 RVA: 0x00006174 File Offset: 0x00004374
			protected override void InsertItem(int index, IconLibrary.IconReference item)
			{
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				if (index != base.Count)
				{
					throw new NotSupportedException();
				}
				if (-1 != this.IndexOf(item.Name))
				{
					throw new ArgumentException("Duplicated icon name:" + item.Name);
				}
				IconLibrary.IconReferenceCollection.InsertImage(this.owner.smallImageList, index, item);
				IconLibrary.IconReferenceCollection.InsertImage(this.owner.largeImageList, index, item);
				base.InsertItem(index, item);
			}

			// Token: 0x0600014E RID: 334 RVA: 0x000061EF File Offset: 0x000043EF
			private static void InsertImage(ImageList imageList, int index, IconLibrary.IconReference item)
			{
				if (imageList != null)
				{
					imageList.Images.Add(item.Name, item.ToBitmap(imageList.ImageSize));
				}
			}

			// Token: 0x0600014F RID: 335 RVA: 0x00006214 File Offset: 0x00004414
			public void Remove(string name)
			{
				int num = this.IndexOf(name);
				if (-1 != num)
				{
					base.RemoveAt(num);
				}
			}

			// Token: 0x06000150 RID: 336 RVA: 0x00006234 File Offset: 0x00004434
			protected override void RemoveItem(int index)
			{
				IconLibrary.IconReferenceCollection.RemoveImage(this.owner.smallImageList, index);
				IconLibrary.IconReferenceCollection.RemoveImage(this.owner.largeImageList, index);
				base.RemoveItem(index);
			}

			// Token: 0x06000151 RID: 337 RVA: 0x0000625F File Offset: 0x0000445F
			private static void RemoveImage(ImageList imageList, int index)
			{
				if (imageList != null)
				{
					imageList.Images[index].Dispose();
					imageList.Images.RemoveAt(index);
				}
			}

			// Token: 0x06000152 RID: 338 RVA: 0x00006284 File Offset: 0x00004484
			protected override void SetItem(int index, IconLibrary.IconReference item)
			{
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				int num = this.IndexOf(item.Name);
				if (-1 != num && index != num)
				{
					throw new ArgumentException("Duplicated icon name:" + item.Name);
				}
				IconLibrary.IconReferenceCollection.SetImage(this.owner.smallImageList, index, item);
				IconLibrary.IconReferenceCollection.SetImage(this.owner.largeImageList, index, item);
				base.SetItem(index, item);
			}

			// Token: 0x06000153 RID: 339 RVA: 0x000062F6 File Offset: 0x000044F6
			private static void SetImage(ImageList imageList, int index, IconLibrary.IconReference item)
			{
				if (imageList != null)
				{
					imageList.Images[index].Dispose();
					imageList.Images[index] = item.ToBitmap(imageList.ImageSize);
					imageList.Images.SetKeyName(index, item.Name);
				}
			}

			// Token: 0x04000139 RID: 313
			private IconLibrary owner;
		}

		// Token: 0x02000008 RID: 8
		public class GlobalIconEditor : IconEditor
		{
			// Token: 0x06000154 RID: 340 RVA: 0x00006338 File Offset: 0x00004538
			public override void PaintValue(PaintValueEventArgs e)
			{
				Icon icon = e.Value as Icon;
				if (icon != null)
				{
					using (Icon icon2 = new Icon(icon, e.Bounds.Size))
					{
						PaintValueEventArgs e2 = new PaintValueEventArgs(e.Context, icon2, e.Graphics, e.Bounds);
						base.PaintValue(e2);
					}
				}
			}
		}

		// Token: 0x02000009 RID: 9
		public class GlobalIconConverter : IconConverter
		{
			// Token: 0x06000156 RID: 342 RVA: 0x000063B0 File Offset: 0x000045B0
			private static bool GetServices(ITypeDescriptorContext context, out ITypeDiscoveryService typeDiscoveryService, out IReferenceService referenceService)
			{
				if (context == null)
				{
					typeDiscoveryService = null;
					referenceService = null;
				}
				else
				{
					typeDiscoveryService = (ITypeDiscoveryService)context.GetService(typeof(ITypeDiscoveryService));
					referenceService = (IReferenceService)context.GetService(typeof(IReferenceService));
				}
				return context != null && typeDiscoveryService != null && null != referenceService;
			}

			// Token: 0x06000157 RID: 343 RVA: 0x00006407 File Offset: 0x00004607
			public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
			{
				return true;
			}

			// Token: 0x06000158 RID: 344 RVA: 0x0000640C File Offset: 0x0000460C
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				ITypeDiscoveryService typeDiscoveryService;
				IReferenceService referenceService;
				return IconLibrary.GlobalIconConverter.GetServices(context, out typeDiscoveryService, out referenceService);
			}

			// Token: 0x06000159 RID: 345 RVA: 0x00006424 File Offset: 0x00004624
			public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				ITypeDiscoveryService typeDiscoveryService;
				IReferenceService referenceService;
				if (IconLibrary.GlobalIconConverter.GetServices(context, out typeDiscoveryService, out referenceService))
				{
					ArrayList arrayList = new ArrayList();
					foreach (object obj in typeDiscoveryService.GetTypes(typeof(object), true))
					{
						Type type = (Type)obj;
						if (type.IsClass && !type.IsAbstract && !type.IsGenericType && !type.IsGenericTypeDefinition && !type.IsImport && !type.IsNested && type.AssemblyQualifiedName != null && TypeDescriptor.GetAttributes(type)[typeof(GeneratedCodeAttribute)] != null)
						{
							PropertyInfo[] properties = type.GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
							foreach (PropertyInfo propertyInfo in properties)
							{
								if (propertyInfo.PropertyType == typeof(Icon))
								{
									Icon icon = referenceService.GetReference(type.Name + "." + propertyInfo.Name) as Icon;
									if (icon != null)
									{
										arrayList.Add(icon);
									}
								}
							}
						}
					}
					return new TypeConverter.StandardValuesCollection(arrayList);
				}
				return base.GetStandardValues(context);
			}

			// Token: 0x0600015A RID: 346 RVA: 0x00006588 File Offset: 0x00004788
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x0600015B RID: 347 RVA: 0x000065A8 File Offset: 0x000047A8
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (value is Icon && destinationType == typeof(string) && context != null)
				{
					IReferenceService referenceService = (IReferenceService)context.GetService(typeof(IReferenceService));
					if (referenceService != null)
					{
						string name = referenceService.GetName(value);
						if (!string.IsNullOrEmpty(name))
						{
							return name;
						}
					}
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}

			// Token: 0x0600015C RID: 348 RVA: 0x00006609 File Offset: 0x00004809
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x0600015D RID: 349 RVA: 0x00006628 File Offset: 0x00004828
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				string text = value as string;
				if (!string.IsNullOrEmpty(text) && context != null)
				{
					IReferenceService referenceService = (IReferenceService)context.GetService(typeof(IReferenceService));
					if (referenceService != null)
					{
						Icon icon = referenceService.GetReference(text) as Icon;
						if (icon != null)
						{
							return icon;
						}
					}
				}
				return base.ConvertFrom(context, culture, value);
			}
		}

		// Token: 0x0200000A RID: 10
		public class IconReferenceEditor : UITypeEditor
		{
			// Token: 0x0600015F RID: 351 RVA: 0x00006683 File Offset: 0x00004883
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			public override bool GetPaintValueSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			// Token: 0x06000160 RID: 352 RVA: 0x00006688 File Offset: 0x00004888
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			public override void PaintValue(PaintValueEventArgs e)
			{
				IconLibrary.IconReference iconReference = e.Value as IconLibrary.IconReference;
				if (iconReference != null)
				{
					this.globalIconEditor.PaintValue(new PaintValueEventArgs(e.Context, iconReference.Icon, e.Graphics, e.Bounds));
				}
			}

			// Token: 0x0400013A RID: 314
			private IconLibrary.GlobalIconEditor globalIconEditor = new IconLibrary.GlobalIconEditor();
		}

		// Token: 0x0200000B RID: 11
		public class IconReferenceConverter : TypeConverter
		{
			// Token: 0x06000162 RID: 354 RVA: 0x000066DF File Offset: 0x000048DF
			public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			// Token: 0x06000163 RID: 355 RVA: 0x000066E4 File Offset: 0x000048E4
			public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
			{
				if (propertyValues == null)
				{
					throw new ArgumentNullException("propertyValues");
				}
				string name = propertyValues["Name"] as string;
				Icon icon = propertyValues["Icon"] as Icon;
				return new IconLibrary.IconReference(name, icon);
			}

			// Token: 0x06000164 RID: 356 RVA: 0x00006728 File Offset: 0x00004928
			public override bool GetPropertiesSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			// Token: 0x06000165 RID: 357 RVA: 0x0000672C File Offset: 0x0000492C
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(IconLibrary.IconReference), attributes);
				return properties.Sort(new string[]
				{
					"Name",
					"Icon"
				});
			}

			// Token: 0x06000166 RID: 358 RVA: 0x00006768 File Offset: 0x00004968
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return destinationType == typeof(InstanceDescriptor) || destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x06000167 RID: 359 RVA: 0x00006798 File Offset: 0x00004998
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				IconLibrary.IconReference iconReference = value as IconLibrary.IconReference;
				if (destinationType == typeof(string))
				{
					if (iconReference == null)
					{
						return "(None)";
					}
					string text = (string)this.globalIconConverter.ConvertTo(context, culture, iconReference.Icon, typeof(string));
					if (iconReference.Name != text)
					{
						return iconReference.Name + ":" + text;
					}
					return text;
				}
				else
				{
					if (destinationType == typeof(InstanceDescriptor) && iconReference != null)
					{
						return new InstanceDescriptor(typeof(IconLibrary.IconReference).GetConstructor(new Type[]
						{
							typeof(string),
							typeof(Icon)
						}), new object[]
						{
							iconReference.Name,
							iconReference.Icon
						}, true);
					}
					return base.ConvertTo(context, culture, value, destinationType);
				}
			}

			// Token: 0x06000168 RID: 360 RVA: 0x00006880 File Offset: 0x00004A80
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x06000169 RID: 361 RVA: 0x000068A0 File Offset: 0x00004AA0
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				string text = value as string;
				if (!string.IsNullOrEmpty(text))
				{
					int num = text.LastIndexOf(':');
					string text2 = text.Substring(num + 1);
					string name = (-1 == num) ? text2 : text.Substring(0, num);
					Icon icon = (Icon)this.globalIconConverter.ConvertFrom(context, culture, text2);
					if (icon != null)
					{
						return new IconLibrary.IconReference(name, icon);
					}
				}
				return base.ConvertFrom(context, culture, value);
			}

			// Token: 0x0600016A RID: 362 RVA: 0x0000690A File Offset: 0x00004B0A
			public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
			{
				return false;
			}

			// Token: 0x0600016B RID: 363 RVA: 0x0000690D File Offset: 0x00004B0D
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return this.globalIconConverter.GetStandardValuesSupported(context);
			}

			// Token: 0x0600016C RID: 364 RVA: 0x0000691C File Offset: 0x00004B1C
			public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				TypeConverter.StandardValuesCollection standardValues = this.globalIconConverter.GetStandardValues(context);
				IconLibrary.IconReference[] array = new IconLibrary.IconReference[standardValues.Count];
				for (int i = 0; i < standardValues.Count; i++)
				{
					string text = this.globalIconConverter.ConvertToInvariantString(context, standardValues[i]);
					string name = text.Substring(1 + text.IndexOf('.'));
					array[i] = new IconLibrary.IconReference(name, (Icon)standardValues[i]);
				}
				return new TypeConverter.StandardValuesCollection(array);
			}

			// Token: 0x0400013B RID: 315
			private IconLibrary.GlobalIconConverter globalIconConverter = new IconLibrary.GlobalIconConverter();
		}

		// Token: 0x0200000C RID: 12
		public class IconReferenceCollectionEditor : CollectionEditor
		{
			// Token: 0x0600016E RID: 366 RVA: 0x000069A8 File Offset: 0x00004BA8
			public IconReferenceCollectionEditor() : base(typeof(IconLibrary.IconReference))
			{
			}

			// Token: 0x0600016F RID: 367 RVA: 0x000069BA File Offset: 0x00004BBA
			protected override CollectionEditor.CollectionForm CreateCollectionForm()
			{
				return new IconLibrary.IconReferenceCollectionEditor.IconCollectionEditorUI(this);
			}

			// Token: 0x0200000D RID: 13
			protected class IconCollectionEditorUI : CollectionEditor.CollectionForm
			{
				// Token: 0x06000170 RID: 368 RVA: 0x000069C2 File Offset: 0x00004BC2
				public IconCollectionEditorUI() : this(new IconLibrary.IconReferenceCollectionEditor())
				{
				}

				// Token: 0x06000171 RID: 369 RVA: 0x000069CF File Offset: 0x00004BCF
				public IconCollectionEditorUI(IconLibrary.IconReferenceCollectionEditor editor) : base(editor)
				{
					this.InitializeComponent();
				}

				// Token: 0x06000172 RID: 370 RVA: 0x000069DE File Offset: 0x00004BDE
				protected override void Dispose(bool disposing)
				{
					if (disposing)
					{
						this.components.Dispose();
					}
					base.Dispose(disposing);
				}

				// Token: 0x06000173 RID: 371 RVA: 0x000069F8 File Offset: 0x00004BF8
				private void InitializeComponent()
				{
					this.components = new Container();
					this.previewPanel = new FlowLayoutPanel();
					this.iconList = new ListView();
					this.iconLibrary = new IconLibrary(this.components);
					ToolStripSeparator toolStripSeparator = new ToolStripSeparator();
					this.duplicateIconButton = new ToolStripButton();
					this.renameIconButton = new ToolStripButton();
					Button button = new Button();
					Button button2 = new Button();
					PictureBox pictureBox = new PictureBox();
					PictureBox pictureBox2 = new PictureBox();
					ToolStrip toolStrip = new ToolStrip();
					ToolStripLabel toolStripLabel = new ToolStripLabel();
					ColumnHeader columnHeader = new ColumnHeader();
					Label label = new Label();
					TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
					((ISupportInitialize)pictureBox).BeginInit();
					((ISupportInitialize)pictureBox2).BeginInit();
					toolStrip.SuspendLayout();
					tableLayoutPanel.SuspendLayout();
					this.previewPanel.SuspendLayout();
					base.SuspendLayout();
					button.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
					button.DialogResult = DialogResult.Cancel;
					button.Location = new Point(202, 7);
					button.Name = "cancelButton";
					button.Size = new Size(75, 23);
					button.TabIndex = 2;
					button.Text = "Cancel";
					button.UseVisualStyleBackColor = true;
					button2.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
					button2.DialogResult = DialogResult.OK;
					button2.Location = new Point(121, 7);
					button2.Name = "okButton";
					button2.Size = new Size(75, 23);
					button2.TabIndex = 1;
					button2.Text = "OK";
					button2.UseVisualStyleBackColor = true;
					pictureBox.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
					pictureBox.Location = new Point(57, 11);
					pictureBox.Name = "preview16";
					pictureBox.Size = new Size(16, 16);
					pictureBox.TabIndex = 0;
					pictureBox.TabStop = false;
					pictureBox2.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
					pictureBox2.Location = new Point(79, 3);
					pictureBox2.Name = "preview32";
					pictureBox2.Size = new Size(32, 32);
					pictureBox2.TabIndex = 1;
					pictureBox2.TabStop = false;
					toolStrip.GripStyle = ToolStripGripStyle.Hidden;
					toolStrip.Items.AddRange(new ToolStripItem[]
					{
						toolStripLabel,
						toolStripSeparator,
						this.duplicateIconButton,
						this.renameIconButton
					});
					toolStrip.Location = new Point(0, 0);
					toolStrip.Name = "toolStrip";
					toolStrip.Size = new Size(280, 25);
					toolStrip.TabIndex = 0;
					toolStrip.Text = "toolStrip1";
					toolStripLabel.Name = "toolStripLabel1";
					toolStripLabel.Size = new Size(175, 22);
					toolStripLabel.Text = "Check icons to include in the library";
					columnHeader.Text = "Name";
					columnHeader.Width = 254;
					label.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
					label.AutoSize = true;
					label.Location = new Point(3, 12);
					label.Name = "label1";
					label.Size = new Size(48, 13);
					label.TabIndex = 0;
					label.Text = "Preview:";
					tableLayoutPanel.AutoSize = true;
					tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
					tableLayoutPanel.ColumnCount = 3;
					tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
					tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
					tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
					tableLayoutPanel.Controls.Add(button2, 1, 0);
					tableLayoutPanel.Controls.Add(button, 2, 0);
					tableLayoutPanel.Controls.Add(this.previewPanel, 0, 0);
					tableLayoutPanel.Dock = DockStyle.Bottom;
					tableLayoutPanel.Location = new Point(0, 354);
					tableLayoutPanel.Name = "tableLayoutPanel";
					tableLayoutPanel.RowCount = 1;
					tableLayoutPanel.RowStyles.Add(new RowStyle());
					tableLayoutPanel.Size = new Size(280, 38);
					tableLayoutPanel.TabIndex = 2;
					this.previewPanel.AutoSize = true;
					this.previewPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
					this.previewPanel.BackColor = Color.Transparent;
					this.previewPanel.Controls.Add(label);
					this.previewPanel.Controls.Add(pictureBox);
					this.previewPanel.Controls.Add(pictureBox2);
					this.previewPanel.Location = new Point(0, 0);
					this.previewPanel.Margin = new Padding(0);
					this.previewPanel.Name = "previewPanel";
					this.previewPanel.Size = new Size(114, 38);
					this.previewPanel.TabIndex = 0;
					this.previewPanel.WrapContents = false;
					this.iconList.CheckBoxes = true;
					this.iconList.Columns.AddRange(new ColumnHeader[]
					{
						columnHeader
					});
					this.iconList.Dock = DockStyle.Fill;
					this.iconList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
					this.iconList.HideSelection = false;
					this.iconList.LabelEdit = true;
					this.iconList.LargeImageList = this.iconLibrary.LargeImageList;
					this.iconList.Location = new Point(0, 25);
					this.iconList.Name = "iconList";
					this.iconList.Size = new Size(280, 329);
					this.iconList.SmallImageList = this.iconLibrary.SmallImageList;
					this.iconList.Sorting = SortOrder.Ascending;
					this.iconList.TabIndex = 1;
					this.iconList.UseCompatibleStateImageBehavior = false;
					this.iconList.View = View.Details;
					this.iconList.SelectedIndexChanged += this.iconList_SelectedIndexChanged;
					this.iconList.AfterLabelEdit += this.iconList_AfterLabelEdit;
					toolStripSeparator.Name = "toolStripSeparator";
					toolStripSeparator.Size = new Size(6, 25);
					this.duplicateIconButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
					this.duplicateIconButton.Name = "duplicateIconButton";
					this.duplicateIconButton.Size = new Size(79, 22);
					this.duplicateIconButton.Text = "Duplicate";
					this.duplicateIconButton.Click += this.duplicateIconButton_Click;
					this.renameIconButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
					this.renameIconButton.Name = "renameIconButton";
					this.renameIconButton.Size = new Size(79, 22);
					this.renameIconButton.Text = "Rename";
					this.renameIconButton.Click += this.renameIconButton_Click;
					base.AcceptButton = button2;
					base.AutoScaleDimensions = new SizeF(6f, 13f);
					base.AutoScaleMode = AutoScaleMode.Font;
					base.CancelButton = button;
					base.ClientSize = new Size(300, 408);
					base.Controls.Add(this.iconList);
					base.Controls.Add(tableLayoutPanel);
					base.Controls.Add(toolStrip);
					base.MaximizeBox = false;
					base.MinimizeBox = false;
					this.MinimumSize = new Size(306, 204);
					base.Name = "IconCollectionEditorUI";
					base.Padding = new Padding(0, 0, 0, 16);
					base.ShowIcon = false;
					base.ShowInTaskbar = false;
					base.SizeGripStyle = SizeGripStyle.Show;
					base.StartPosition = FormStartPosition.CenterParent;
					this.Text = "IconLibrary Icons Editor";
					((ISupportInitialize)pictureBox).EndInit();
					((ISupportInitialize)pictureBox2).EndInit();
					toolStrip.ResumeLayout(false);
					toolStrip.PerformLayout();
					tableLayoutPanel.ResumeLayout(false);
					tableLayoutPanel.PerformLayout();
					this.previewPanel.ResumeLayout(false);
					this.previewPanel.PerformLayout();
					base.ResumeLayout(false);
					base.PerformLayout();
				}

				// Token: 0x06000174 RID: 372 RVA: 0x00007194 File Offset: 0x00005394
				protected override void OnEditValueChanged()
				{
					this.iconList.BeginUpdate();
					try
					{
						this.collectionToEdit = (IconLibrary.IconReferenceCollection)base.EditValue;
						if (this.collectionToEdit != null)
						{
							foreach (IconLibrary.IconReference iconReference in this.collectionToEdit)
							{
								ListViewItem listViewItem = this.AddIconReference(iconReference);
								listViewItem.Checked = true;
							}
							this.PopulateIconsFromProjectResources();
							if (this.iconList.Items.Count > 0)
							{
								this.iconList.Items[0].Focused = true;
								this.iconList.Items[0].Selected = true;
							}
							this.iconList.Select();
						}
						else
						{
							this.iconLibrary.icons.Clear();
							this.iconList.Items.Clear();
						}
					}
					finally
					{
						this.iconList.EndUpdate();
					}
				}

				// Token: 0x06000175 RID: 373 RVA: 0x000072A0 File Offset: 0x000054A0
				private void PopulateIconsFromProjectResources()
				{
					IconLibrary.IconReferenceConverter iconReferenceConverter = new IconLibrary.IconReferenceConverter();
					foreach (object obj in iconReferenceConverter.GetStandardValues(base.Context))
					{
						IconLibrary.IconReference iconReference = (IconLibrary.IconReference)obj;
						if (-1 == this.collectionToEdit.IndexOf(iconReference.Icon))
						{
							this.AddIconWithUniqueName(iconReference.Name, iconReference.Icon);
						}
					}
				}

				// Token: 0x06000176 RID: 374 RVA: 0x00007330 File Offset: 0x00005530
				protected override void OnFormClosing(FormClosingEventArgs e)
				{
					if (base.DialogResult == DialogResult.OK)
					{
						bool flag = false;
						StringCollection stringCollection = new StringCollection();
						foreach (object obj in this.iconList.CheckedItems)
						{
							ListViewItem listViewItem = (ListViewItem)obj;
							string value = listViewItem.Text.ToLowerInvariant();
							if (stringCollection.Contains(value))
							{
								flag = true;
								this.DisplayError(new Exception("There are duplicated names included in the library.\r\nEither rename one of the duplicated references or exclude it from the library by unchecking the item."));
								e.Cancel = true;
								listViewItem.BeginEdit();
								break;
							}
							stringCollection.Add(value);
						}
						if (!flag)
						{
							ArrayList arrayList = new ArrayList();
							foreach (object obj2 in this.iconList.CheckedItems)
							{
								ListViewItem listViewItem2 = (ListViewItem)obj2;
								arrayList.Add(IconLibrary.IconReferenceCollectionEditor.IconCollectionEditorUI.GetIconReference(listViewItem2));
							}
							base.Items = arrayList.ToArray();
						}
					}
					base.OnFormClosing(e);
				}

				// Token: 0x06000177 RID: 375 RVA: 0x0000745C File Offset: 0x0000565C
				private ListViewItem AddIconWithUniqueName(string baseName, Icon icon)
				{
					int num = 1;
					string text = baseName;
					for (;;)
					{
						ListViewItem listViewItem = this.iconList.Items[text];
						if (listViewItem == null)
						{
							break;
						}
						num++;
						text = baseName + num.ToString();
					}
					return this.AddIconReference(new IconLibrary.IconReference(text, icon));
				}

				// Token: 0x06000178 RID: 376 RVA: 0x000074A4 File Offset: 0x000056A4
				private ListViewItem AddIconReference(IconLibrary.IconReference iconReference)
				{
					this.iconLibrary.Icons.Add(this.iconLibrary.Icons.Count.ToString(), iconReference.Icon);
					ListViewItem listViewItem = this.iconList.Items.Add(iconReference.Name, iconReference.Name, this.iconLibrary.Icons.Count - 1);
					listViewItem.Tag = iconReference;
					return listViewItem;
				}

				// Token: 0x06000179 RID: 377 RVA: 0x00007516 File Offset: 0x00005716
				private static IconLibrary.IconReference GetIconReference(ListViewItem listViewItem)
				{
					return (IconLibrary.IconReference)listViewItem.Tag;
				}

				// Token: 0x0600017A RID: 378 RVA: 0x00007524 File Offset: 0x00005724
				private void iconList_AfterLabelEdit(object sender, LabelEditEventArgs e)
				{
					if (string.IsNullOrEmpty(e.Label))
					{
						e.CancelEdit = true;
						return;
					}
					ListViewItem listViewItem = this.iconList.Items[e.Item];
					IconLibrary.IconReference iconReference = IconLibrary.IconReferenceCollectionEditor.IconCollectionEditorUI.GetIconReference(listViewItem);
					listViewItem.Name = e.Label;
					listViewItem.Tag = new IconLibrary.IconReference(e.Label, iconReference.Icon);
				}

				// Token: 0x0600017B RID: 379 RVA: 0x00007588 File Offset: 0x00005788
				private void iconList_SelectedIndexChanged(object sender, EventArgs e)
				{
					bool enabled = this.iconList.SelectedItems.Count == 1;
					this.duplicateIconButton.Enabled = enabled;
					this.renameIconButton.Enabled = enabled;
					IconLibrary.IconReference iconReference = null;
					if (this.iconList.SelectedItems.Count != 0)
					{
						iconReference = IconLibrary.IconReferenceCollectionEditor.IconCollectionEditorUI.GetIconReference(this.iconList.SelectedItems[0]);
					}
					foreach (object obj in this.previewPanel.Controls)
					{
						Control control = (Control)obj;
						PictureBox pictureBox = control as PictureBox;
						if (pictureBox != null)
						{
							if (pictureBox.Image != null)
							{
								pictureBox.Image.Dispose();
								pictureBox.Image = null;
							}
							if (iconReference != null)
							{
								pictureBox.Image = iconReference.ToBitmap(pictureBox.Size);
							}
						}
					}
				}

				// Token: 0x0600017C RID: 380 RVA: 0x00007678 File Offset: 0x00005878
				[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
				{
					bool flag = false;
					if (keyData == Keys.F2)
					{
						if (this.iconList.FocusedItem != null)
						{
							this.iconList.FocusedItem.BeginEdit();
							flag = true;
						}
					}
					else
					{
						flag = false;
					}
					if (!flag)
					{
						flag = base.ProcessCmdKey(ref msg, keyData);
					}
					return flag;
				}

				// Token: 0x0600017D RID: 381 RVA: 0x000076C0 File Offset: 0x000058C0
				private void duplicateIconButton_Click(object sender, EventArgs e)
				{
					ListViewItem listViewItem = this.iconList.SelectedItems[0];
					listViewItem.Selected = false;
					IconLibrary.IconReference iconReference = IconLibrary.IconReferenceCollectionEditor.IconCollectionEditorUI.GetIconReference(listViewItem);
					ListViewItem listViewItem2 = this.AddIconWithUniqueName(iconReference.Name.TrimEnd(new char[]
					{
						'0',
						'1',
						'2',
						'3',
						'4',
						'5',
						'6',
						'7',
						'8',
						'9'
					}), iconReference.Icon);
					listViewItem2.Selected = true;
					listViewItem2.Checked = true;
					listViewItem2.BeginEdit();
				}

				// Token: 0x0600017E RID: 382 RVA: 0x00007755 File Offset: 0x00005955
				private void renameIconButton_Click(object sender, EventArgs e)
				{
					this.iconList.SelectedItems[0].BeginEdit();
				}

				// Token: 0x0400013C RID: 316
				private IContainer components;

				// Token: 0x0400013D RID: 317
				private ListView iconList;

				// Token: 0x0400013E RID: 318
				private IconLibrary iconLibrary;

				// Token: 0x0400013F RID: 319
				private FlowLayoutPanel previewPanel;

				// Token: 0x04000140 RID: 320
				private ToolStripButton duplicateIconButton;

				// Token: 0x04000141 RID: 321
				private ToolStripButton renameIconButton;

				// Token: 0x04000142 RID: 322
				private IconLibrary.IconReferenceCollection collectionToEdit;
			}
		}
	}
}
