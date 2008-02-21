using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rubicon.Data.Linq.DataObjectModel;

namespace Rubicon.Data.Linq.UnitTests.DataObjectModelTest
{
  [TestFixture]
  public class FieldSourcePathTest
  {
    [Test]
    public void Initialization ()
    {
      Table source = new Table();
      SingleJoin join1 = new SingleJoin(new Column(source,"s1"),new Column(source,"s2"));
      SingleJoin join2 = new SingleJoin (new Column (source, "s3"), new Column (source, "s4"));
      SingleJoin[] joins = new[] {join1,join2};
      
      FieldSourcePath sourcePath = new FieldSourcePath(source,joins);

      Assert.AreSame (source, sourcePath.SourceTable);
      Assert.That (sourcePath.Joins, Is.EqualTo (joins));
    }

    [Test]
    public void Equals_True()
    {
      Table source = new Table ("source", "s");
      SingleJoin join1 = new SingleJoin (new Column (source, "s1"), new Column (source, "s2"));
      SingleJoin join2 = new SingleJoin (new Column (source, "s3"), new Column (source, "s4"));
      SingleJoin[] joins = new[] { join1, join2 };

      FieldSourcePath sourcePath1 = new FieldSourcePath (source, joins);
      FieldSourcePath sourcePath2 = new FieldSourcePath (source, joins);

      Assert.AreEqual (sourcePath1, sourcePath2);
    }

    [Test]
    public void Equals_FalseOtherClass ()
    {
      Table source = new Table ("source", "s");
      SingleJoin join1 = new SingleJoin (new Column (source, "s1"), new Column (source, "s2"));
      SingleJoin join2 = new SingleJoin (new Column (source, "s3"), new Column (source, "s4"));
      SingleJoin[] joins = new[] { join1, join2 };

      FieldSourcePath sourcePath = new FieldSourcePath (source, joins);

      Assert.AreNotEqual (new object(), sourcePath);
    }

    [Test]
    public void Equals_FalseTable ()
    {
      Table source1 = new Table ("source", "s");
      Table source2 = new Table ("source", "s");
      SingleJoin join1 = new SingleJoin (new Column (source1, "s1"), new Column (source1, "s2"));
      SingleJoin join2 = new SingleJoin (new Column (source1, "s3"), new Column (source1, "s4"));
      SingleJoin[] joins = new[] { join1, join2 };

      FieldSourcePath sourcePath1 = new FieldSourcePath (source1, joins);
      FieldSourcePath sourcePath2 = new FieldSourcePath (source2, joins);

      Assert.AreNotEqual (sourcePath1, sourcePath2);
    }

    [Test]
    public void Equals_FalseJoins ()
    {
      Table source = new Table ("source", "s");
      SingleJoin join1 = new SingleJoin (new Column (source, "s1"), new Column (source, "s2"));
      SingleJoin join2 = new SingleJoin (new Column (source, "s3"), new Column (source, "s4"));
      SingleJoin join3 = new SingleJoin (new Column (source, "s5"), new Column (source, "s6"));
      SingleJoin[] joins1 = new[] { join1, join2 };
      SingleJoin[] joins2 = new[] { join1, join3 };

      FieldSourcePath sourcePath1 = new FieldSourcePath (source, joins1);
      FieldSourcePath sourcePath2 = new FieldSourcePath (source, joins2);

      Assert.AreNotEqual (sourcePath1, sourcePath2);
    }

    [Test]
    public void GetHashCode_EqualPaths ()
    {
      Table source = new Table ("source", "s");
      SingleJoin join1 = new SingleJoin (new Column (source, "s1"), new Column (source, "s2"));
      SingleJoin join2 = new SingleJoin (new Column (source, "s3"), new Column (source, "s4"));
      SingleJoin[] joins = new[] { join1, join2 };

      FieldSourcePath sourcePath1 = new FieldSourcePath (source, joins);
      FieldSourcePath sourcePath2 = new FieldSourcePath (source, joins);

      Assert.AreEqual (sourcePath1.GetHashCode(), sourcePath2.GetHashCode());
    }

    [Test]
    public void GetHashCode_DifferentTables ()
    {
      Table source1 = new Table ("source", "s");
      Table source2 = new Table ("source", "s");
      SingleJoin join1 = new SingleJoin (new Column (source1, "s1"), new Column (source1, "s2"));
      SingleJoin join2 = new SingleJoin (new Column (source1, "s3"), new Column (source1, "s4"));
      SingleJoin[] joins = new[] { join1, join2 };

      FieldSourcePath sourcePath1 = new FieldSourcePath (source1, joins);
      FieldSourcePath sourcePath2 = new FieldSourcePath (source2, joins);

      Assert.AreEqual (sourcePath1.GetHashCode(), sourcePath2.GetHashCode());
    }

    [Test]
    public void GetHashCode_DifferentJoins ()
    {
      Table source = new Table ("source", "s");
      SingleJoin join1 = new SingleJoin (new Column (source, "s1"), new Column (source, "s2"));
      SingleJoin join2 = new SingleJoin (new Column (source, "s3"), new Column (source, "s4"));
      SingleJoin join3 = new SingleJoin (new Column (source, "s5"), new Column (source, "s6"));
      SingleJoin[] joins1 = new[] { join1, join2 };
      SingleJoin[] joins2 = new[] { join1, join3 };

      FieldSourcePath sourcePath1 = new FieldSourcePath (source, joins1);
      FieldSourcePath sourcePath2 = new FieldSourcePath (source, joins2);

      Assert.AreNotEqual (sourcePath1.GetHashCode(), sourcePath2.GetHashCode());
    }
  }
}