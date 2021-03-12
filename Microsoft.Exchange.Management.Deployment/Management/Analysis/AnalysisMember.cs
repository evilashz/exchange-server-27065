using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Analysis.Features;

namespace Microsoft.Exchange.Management.Analysis
{
	// Token: 0x0200003B RID: 59
	internal abstract class AnalysisMember : IXmlSerializable
	{
		// Token: 0x06000150 RID: 336 RVA: 0x000065BC File Offset: 0x000047BC
		public AnalysisMember(Func<AnalysisMember> parent, ConcurrencyType runAs, Analysis analysis, IEnumerable<Feature> features)
		{
			this.parent = new Lazy<AnalysisMember>(delegate()
			{
				if (parent != null)
				{
					return parent();
				}
				return analysis.RootAnalysisMember;
			}, true);
			this.RunAs = runAs;
			this.Analysis = analysis;
			this.features = new List<Feature>(features);
			this.featuresHaveBeenInherited = false;
			this.startTimeTicks = default(DateTime).Ticks;
			this.stopTimeTicks = default(DateTime).Ticks;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00006659 File Offset: 0x00004859
		public AnalysisMember Parent
		{
			get
			{
				return this.parent.Value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00006666 File Offset: 0x00004866
		// (set) Token: 0x06000153 RID: 339 RVA: 0x0000666E File Offset: 0x0000486E
		public ConcurrencyType RunAs { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00006677 File Offset: 0x00004877
		// (set) Token: 0x06000155 RID: 341 RVA: 0x0000667F File Offset: 0x0000487F
		public Analysis Analysis { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000156 RID: 342
		public abstract Type ValueType { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00006688 File Offset: 0x00004888
		// (set) Token: 0x06000158 RID: 344 RVA: 0x000066AC File Offset: 0x000048AC
		public ExDateTime StartTime
		{
			get
			{
				long ticks = Thread.VolatileRead(ref this.startTimeTicks);
				return new ExDateTime(ExTimeZone.UtcTimeZone, ticks);
			}
			protected set
			{
				long utcTicks = value.UtcTicks;
				Interlocked.Exchange(ref this.startTimeTicks, utcTicks);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000066D0 File Offset: 0x000048D0
		// (set) Token: 0x0600015A RID: 346 RVA: 0x000066F4 File Offset: 0x000048F4
		public ExDateTime StopTime
		{
			get
			{
				long ticks = Thread.VolatileRead(ref this.stopTimeTicks);
				return new ExDateTime(ExTimeZone.UtcTimeZone, ticks);
			}
			protected set
			{
				long utcTicks = value.UtcTicks;
				Interlocked.Exchange(ref this.stopTimeTicks, utcTicks);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00006718 File Offset: 0x00004918
		public IEnumerable<Feature> Features
		{
			get
			{
				IEnumerable<Feature> result;
				lock (this.features)
				{
					if (this.featuresHaveBeenInherited)
					{
						result = this.features;
					}
					else
					{
						this.InheritFeatures();
						this.CombineModes();
						this.CombineRoles();
						this.featuresHaveBeenInherited = true;
						result = this.features;
					}
				}
				return result;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00006784 File Offset: 0x00004984
		public string Name
		{
			get
			{
				return this.Analysis.GetAnalysisMemberName(this);
			}
		}

		// Token: 0x0600015D RID: 349
		public abstract void Start();

		// Token: 0x0600015E RID: 350
		public abstract IEnumerable<Result> GetResults();

		// Token: 0x0600015F RID: 351 RVA: 0x00006884 File Offset: 0x00004A84
		public IEnumerable<AnalysisMember> AncestorsAndSelf()
		{
			for (AnalysisMember current = this; current != null; current = current.Parent)
			{
				yield return current;
			}
			yield break;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000068CC File Offset: 0x00004ACC
		private void InheritFeatures()
		{
			if (this.Parent == null)
			{
				return;
			}
			using (IEnumerator<Feature> enumerator = (from y in this.Parent.Features
			where y.IsInheritable
			select y).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Feature feature = enumerator.Current;
					if (!feature.AllowsMultiple)
					{
						if (!(from x in this.features
						where x.GetType() == feature.GetType()
						select x).Any<Feature>())
						{
							this.features.Add(feature);
						}
					}
					else
					{
						this.features.Add(feature);
					}
				}
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000069A4 File Offset: 0x00004BA4
		private void CombineModes()
		{
			AppliesToModeFeature[] array = this.features.OfType<AppliesToModeFeature>().ToArray<AppliesToModeFeature>();
			if (array.Length > 0)
			{
				foreach (AppliesToModeFeature item in array)
				{
					this.features.Remove(item);
				}
				SetupMode setupMode = SetupMode.None;
				foreach (AppliesToModeFeature appliesToModeFeature in array)
				{
					setupMode |= appliesToModeFeature.Mode;
				}
				this.features.Add(new AppliesToModeFeature(setupMode));
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006A2C File Offset: 0x00004C2C
		private void CombineRoles()
		{
			AppliesToRoleFeature[] array = this.features.OfType<AppliesToRoleFeature>().ToArray<AppliesToRoleFeature>();
			if (array.Length > 0)
			{
				foreach (AppliesToRoleFeature item in array)
				{
					this.features.Remove(item);
				}
				SetupRole setupRole = SetupRole.None;
				foreach (AppliesToRoleFeature appliesToRoleFeature in array)
				{
					setupRole |= appliesToRoleFeature.Role;
				}
				this.features.Add(new AppliesToRoleFeature(setupRole));
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006AB1 File Offset: 0x00004CB1
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006AB4 File Offset: 0x00004CB4
		public void ReadXml(XmlReader reader)
		{
			throw new NotSupportedException("Read from XML is not supported.");
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006AC0 File Offset: 0x00004CC0
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement((this is Rule) ? "rule" : "setting");
			writer.WriteStartAttribute("name");
			writer.WriteValue(this.Name);
			writer.WriteEndAttribute();
			writer.WriteStartAttribute("type");
			writer.WriteValue(this.ValueType.FullName);
			writer.WriteEndAttribute();
			writer.WriteStartAttribute("starttime");
			writer.WriteValue(this.StartTime.ToString());
			writer.WriteEndAttribute();
			writer.WriteStartAttribute("stoptime");
			writer.WriteValue(this.StopTime.ToString());
			writer.WriteEndAttribute();
			writer.WriteStartElement("features");
			foreach (Feature feature in this.Features)
			{
				writer.WriteStartElement("feature");
				IXmlSerializable xmlSerializable = feature as IXmlSerializable;
				if (xmlSerializable != null)
				{
					xmlSerializable.WriteXml(writer);
				}
				else
				{
					writer.WriteValue(feature.ToString());
				}
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
			writer.WriteStartElement("results");
			foreach (Result result in this.GetResults())
			{
				writer.WriteStartElement("result");
				if (result.HasException)
				{
					writer.WriteStartElement("exception");
					writer.WriteValue(result.Exception.ToString());
				}
				else
				{
					writer.WriteStartElement("value");
					IXmlSerializable xmlSerializable2 = result.ValueAsObject as IXmlSerializable;
					if (xmlSerializable2 != null)
					{
						xmlSerializable2.WriteXml(writer);
					}
					else
					{
						object valueAsObject = result.ValueAsObject;
						if (valueAsObject != null)
						{
							writer.WriteValue(result.ValueAsObject.ToString());
						}
					}
				}
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x040000F1 RID: 241
		private Lazy<AnalysisMember> parent;

		// Token: 0x040000F2 RID: 242
		private List<Feature> features;

		// Token: 0x040000F3 RID: 243
		private bool featuresHaveBeenInherited;

		// Token: 0x040000F4 RID: 244
		private long startTimeTicks;

		// Token: 0x040000F5 RID: 245
		private long stopTimeTicks;
	}
}
