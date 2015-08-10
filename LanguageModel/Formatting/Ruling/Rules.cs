﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace LanguageService.Formatting.Ruling
{
    internal class Rules
    {
        private static readonly List<Func<FormattingContext, bool>> defaultFilters = new List<Func<FormattingContext, bool>>
        {
            TokensAreOnSameLine,
            NoCommentsBetweenTokens
        };

        internal static readonly Rule SpaceAfterComma =
            new SimpleRule(
                new RuleDescriptor(SyntaxKind.Comma, TokenRange.AnyVisible),
                defaultFilters, RuleAction.Space);

        internal static readonly Rule SpaceAfterAssignmentOperatorInStatement =
            new SimpleRule(
                new RuleDescriptor(SyntaxKind.AssignmentOperator, TokenRange.AnyVisible),
                new List<Func<FormattingContext, bool>>
                {
                    TokensAreOnSameLine,
                    NoCommentsBetweenTokens,
                    InSyntaxNode(Side.Left, new List<SyntaxKind>
                    {
                        SyntaxKind.AssignmentStatementNode,
                        SyntaxKind.LocalAssignmentStatementNode
                    })
                },
                RuleAction.Space);

        internal static readonly Rule SpaceBeforeAssignmentOperatorInStatement =
            new SimpleRule(
                new RuleDescriptor(TokenRange.AnyVisible, SyntaxKind.AssignmentOperator),
                new List<Func<FormattingContext, bool>>
                {
                    TokensAreOnSameLine,
                    NoCommentsBetweenTokens,
                    InSyntaxNode(Side.Right, new List<SyntaxKind>
                    {
                        SyntaxKind.AssignmentStatementNode,
                        SyntaxKind.LocalAssignmentStatementNode
                    })
                },
                RuleAction.Space);

        internal static readonly Rule SpaceAfterAssignmentOperatorInField =
            new SimpleRule(
                new RuleDescriptor(SyntaxKind.AssignmentOperator, TokenRange.AnyVisible),
                new List<Func<FormattingContext, bool>>
                {
                    TokensAreOnSameLine,
                    NoCommentsBetweenTokens,
                    IsInATableConstructor(Side.Left)
                },
                RuleAction.Space);

        internal static readonly Rule SpaceBeforeAssignmentOperatorInField =
            new SimpleRule(
                new RuleDescriptor(TokenRange.AnyVisible, SyntaxKind.AssignmentOperator),
                new List<Func<FormattingContext, bool>>
                {
                    TokensAreOnSameLine,
                    NoCommentsBetweenTokens,
                    IsInATableConstructor(Side.Right)
                },
                RuleAction.Space);

        internal static readonly Rule SpaceAfterBinaryOperator =
            new SimpleRule(
                new RuleDescriptor(TokenRange.BinaryOperators, TokenRange.AnyVisible),
                defaultFilters, RuleAction.Space);

        internal static readonly Rule SpaceBeforeBinaryOperator =
            new SimpleRule(
                new RuleDescriptor(TokenRange.AnyVisible, TokenRange.BinaryOperators),
                defaultFilters, RuleAction.Space);

        internal static readonly Rule SpaceAfterValueBeforeOpenParenthesis =
            new SimpleRule(
                new RuleDescriptor(TokenRange.Value, SyntaxKind.OpenParen),
                defaultFilters, RuleAction.Space);

        internal static readonly Rule SpaceBeforeValueAfterOpenParenthesis =
            new SimpleRule(
                new RuleDescriptor(SyntaxKind.OpenParen, TokenRange.Value),
                defaultFilters, RuleAction.Space);

        internal static readonly Rule SpaceBeforeValueAfterOpenSquareBracket =
            new SimpleRule(
                new RuleDescriptor(SyntaxKind.OpenBracket, TokenRange.Value),
                defaultFilters, RuleAction.Space);

        internal static readonly Rule SpaceBeforeValueAfterOpenCurlyBrace =
            new SimpleRule(new RuleDescriptor(SyntaxKind.OpenCurlyBrace, TokenRange.Value),
                defaultFilters, RuleAction.Space);

        internal static readonly Rule SpaceAfterValueBeforeCloseParenthesis =
            new SimpleRule(new RuleDescriptor(TokenRange.Value, SyntaxKind.CloseParen),
                defaultFilters, RuleAction.Space);

        internal static readonly Rule SpaceAfterValueBeforeCloseSquareBracket =
            new SimpleRule(new RuleDescriptor(TokenRange.Value, SyntaxKind.CloseBracket),
                defaultFilters, RuleAction.Space);

        internal static readonly Rule SpaceAfterValueBeforeCloseCurlyBrace =
            new SimpleRule(new RuleDescriptor(TokenRange.Value, SyntaxKind.CloseCurlyBrace),
                defaultFilters, RuleAction.Space);

        internal static readonly DeleteSpaceBeforeEofToken DeleteSpaceBeforeEofToken = new DeleteSpaceBeforeEofToken();

        internal static readonly Rule DeleteSpaceAfterValueBeforeDot =
            new SimpleRule(new RuleDescriptor(TokenRange.Value, SyntaxKind.Dot),
                defaultFilters, RuleAction.Delete);

        internal static readonly Rule DeleteSpaceBeforeValueAfterDot =
            new SimpleRule(new RuleDescriptor(SyntaxKind.Dot, TokenRange.Value),
                defaultFilters, RuleAction.Delete);

        internal static readonly Rule DeleteSpaceAfterValueBeforeColon =
            new SimpleRule(new RuleDescriptor(TokenRange.Value, SyntaxKind.Colon),
                defaultFilters, RuleAction.Delete);

        internal static readonly Rule DeleteSpaceBeforeValueAfterColon =
            new SimpleRule(new RuleDescriptor(SyntaxKind.Colon, TokenRange.Value),
                defaultFilters, RuleAction.Delete);

        internal static readonly Rule DeleteTrailingWhitespace = new DeleteTrailingWhitespace();

        internal static readonly Rule NoSpaceAfterCommaInFor =
            new SimpleRule(new RuleDescriptor(SyntaxKind.Comma, TokenRange.AnyVisible),
                new List<Func<FormattingContext, bool>>
                {
                    TokensAreOnSameLine,
                    NoCommentsBetweenTokens,
                    InSyntaxNode(Side.Left, new List<SyntaxKind> { SyntaxKind.SimpleForStatementNode })
                },
                RuleAction.Delete);

        internal static readonly Rule SpaceBeforeAssignmentOperatorInFor =
            new SimpleRule(new RuleDescriptor(TokenRange.AnyVisible, SyntaxKind.AssignmentOperator),
                new List<Func<FormattingContext, bool>>
                {
                    TokensAreNotOnSameLine,
                    NoCommentsBetweenTokens,
                    InSyntaxNode(Side.Right, new List<SyntaxKind> { SyntaxKind.SimpleForStatementNode })
                },
                RuleAction.Space);

        internal static readonly Rule SpaceAfterAssignmentOperatorInFor =
            new SimpleRule(new RuleDescriptor(SyntaxKind.AssignmentOperator, TokenRange.AnyVisible),
                new List<Func<FormattingContext, bool>>
                {
                    TokensAreNotOnSameLine,
                    NoCommentsBetweenTokens,
                    InSyntaxNode(Side.Left, new List<SyntaxKind> { SyntaxKind.SimpleForStatementNode })
                },
                RuleAction.Space);



        internal static readonly ImmutableArray<Rule> AllRules = ImmutableArray.Create(
            NoSpaceAfterCommaInFor,
            SpaceAfterComma,

            SpaceAfterAssignmentOperatorInStatement,
            SpaceBeforeAssignmentOperatorInStatement,
            SpaceBeforeAssignmentOperatorInFor,
            SpaceAfterAssignmentOperatorInFor,
            SpaceBeforeAssignmentOperatorInField,
            SpaceAfterAssignmentOperatorInField,

            SpaceAfterBinaryOperator,
            SpaceBeforeBinaryOperator,

            SpaceAfterValueBeforeOpenParenthesis,
            SpaceBeforeValueAfterOpenParenthesis,
            SpaceBeforeValueAfterOpenSquareBracket,
            SpaceBeforeValueAfterOpenCurlyBrace,
            SpaceAfterValueBeforeCloseParenthesis,
            SpaceAfterValueBeforeCloseSquareBracket,
            SpaceAfterValueBeforeCloseCurlyBrace,

            DeleteSpaceAfterValueBeforeDot,
            DeleteSpaceBeforeValueAfterDot,
            DeleteSpaceAfterValueBeforeColon,
            DeleteSpaceBeforeValueAfterColon,

            DeleteSpaceBeforeEofToken,
            DeleteTrailingWhitespace
            );

        internal static bool TokensAreOnSameLine(FormattingContext formattingContext)
        {
            return formattingContext.TokensOnSameLine();
        }

        internal static bool TokensAreNotOnSameLine(FormattingContext formattingContext)
        {
            return !formattingContext.TokensOnSameLine();
        }

        internal static bool NoCommentsBetweenTokens(FormattingContext formattingContext)
        {
            return !formattingContext.ContainsCommentsBetweenTokens();
        }

        private enum Side
        {
            Left,
            Right
        }

        private static ParsedToken GetTokenOn(Side side, FormattingContext formattingContext)
        {
            return (side == Side.Left) ?
                    formattingContext.CurrentToken :
                    formattingContext.NextToken;
        }

        private static Func<FormattingContext, bool> IsInATableConstructor(Side side)
        {
            return (FormattingContext formattingContext) =>
            {
                ParsedToken parsedToken = GetTokenOn(side, formattingContext);

                return parsedToken.InTableConstructor;
            };
        }

        private static Func<FormattingContext, bool> InSyntaxNode(Side side, List<SyntaxKind> statementKinds)
        {
            return (FormattingContext formattingContext) =>
            {
                ParsedToken parsedToken = GetTokenOn(side, formattingContext);

                return statementKinds.Contains(parsedToken.StatementNode.Kind);
            };
        }

        private static Func<FormattingContext, bool> IsStartOfStatement(Side side)
        {
            return (FormattingContext formattingContext) =>
            {
                ParsedToken parsedToken = GetTokenOn(side, formattingContext);

                return parsedToken.Token.Start == parsedToken.StatementNode.StartPosition;
            };
        }

        //private static bool StatementOnOneLine(FormattingContext formattingContext)
        //{
        //    return formattingContext.CurrentToken.StatementNode.
        //}

    }
}
