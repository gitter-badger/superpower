﻿using Superpower.Parsers;
using Superpower.Tests.Support;
using Xunit;

namespace Superpower.Tests.Combinators
{
    public class TryCombinatorTests
    {
        [Fact]
        public void TryFailureConsumesNoInput()
        {
            var tryAb = Character.EqualTo('a').Then(_ => Character.EqualTo('b')).Try();
            var result = tryAb.TryParse("ac");
            Assert.False(result.HasValue);
            Assert.Equal(0, result.Remainder.Position.Absolute);
        }

        [Fact]
        public void TrySuccessIsTransparent()
        {
            var tryAb = Character.EqualTo('a').Then(_ => Character.EqualTo('b')).Try();
            var result = tryAb.TryParse("ab");
            Assert.True(result.HasValue);
            Assert.True(result.Remainder.IsAtEnd);
        }

        [Fact]
        public void TryItemMakesManyBacktrack()
        {
            var ab = Character.EqualTo('a').Then(_ => Character.EqualTo('b'));
            var list = ab.Try().Many();
            AssertParser.SucceedsWithMany(list, "ababa", "bb".ToCharArray());
        }

        [Fact]
        public void TryAlternativeMakesOrBacktrack()
        {
            var tryAOrAB = Character.EqualTo('a').Then(_ => Character.EqualTo('b')).Try().Or(Character.EqualTo('a'));
            AssertParser.SucceedsWith(tryAOrAB, "a", 'a');
        }

        [Fact]
        public void TokenTryFailureConsumesNoInput()
        {
            var tryAb = Parse.Token('a').Then(_ => Parse.Token('b')).Try();
            var result = tryAb.TryParse(StringAsCharTokenList.Tokenize("ac"));
            Assert.False(result.HasValue);
            Assert.Equal(0, result.Remainder.Position);
        }

        [Fact]
        public void TokenTrySuccessIsTransparent()
        {
            var tryAb = Parse.Token('a').Then(_ => Parse.Token('b')).Try();
            var result = tryAb.TryParse(StringAsCharTokenList.Tokenize("ab"));
            Assert.True(result.HasValue);
            Assert.True(result.Remainder.IsAtEnd);
        }

        [Fact]
        public void TokenTryItemMakesManyBacktrack()
        {
            var ab = Parse.Token('a').Then(_ => Parse.Token('b'));
            var list = ab.Try().Many();
            AssertParser.SucceedsWithMany(list, "ababa", "bb".ToCharArray());
        }

        [Fact]
        public void TokenTryAlternativeMakesOrBacktrack()
        {
            var tryAOrAB = Parse.Token('a').Then(_ => Parse.Token('b')).Try().Or(Parse.Token('a'));
            AssertParser.SucceedsWith(tryAOrAB, "a", 'a');
        }
    }
}
