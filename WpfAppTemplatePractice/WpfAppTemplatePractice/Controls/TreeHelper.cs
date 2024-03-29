﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace WpfAppTemplatePractice.Controls
{
    /// <summary>
    /// Helper methods for UI-related tasks.
    /// This class was obtained from Philip Sumi (a fellow WPF Disciples blog)
    /// http://www.hardcodet.net/uploads/2009/06/UIHelper.cs
    /// </summary>
    public static class TreeHelper
    {
        /// <summary>
        /// Finds a parent of a given item on the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="child">A direct or indirect child of the
        /// queried item.</param>
        /// <returns>The first parent item that matches the submitted
        /// type parameter. If not matching item can be found, a null
        /// reference is being returned.</returns>
        public static T? TryFindParent<T>(this DependencyObject child)
            where T : DependencyObject
        {
            // get parent item
            var parentObject = GetParentObject(child);
            while (parentObject is not null)
            {
                // check if the parent matches the type we're looking for
                if (parentObject is T objectToFind)
                {
                    return objectToFind;
                }

                parentObject = GetParentObject(parentObject);
            }

            // we've reached the end of the tree
            return null;
        }

        /// <summary>
        /// Finds all Ancestors of a given item on the visual tree.
        /// </summary>
        /// <param name="child">A node in a visual tree</param>
        /// <returns>All ancestors in visual tree of <paramref name="child"/> element</returns>
        public static IEnumerable<DependencyObject> GetAncestors(this DependencyObject child)
        {
            var parent = VisualTreeHelper.GetParent(child);
            while (parent is not null)
            {
                yield return parent;
                parent = VisualTreeHelper.GetParent(parent);
            }
        }

        /// <summary>
        /// Returns full visual ancestry, starting at the leaf.
        /// <para>If element is not of <see cref="Visual"/> or <see cref="Visual3D"/> the logical ancestry is used.</para>
        /// </summary>
        /// <param name="leaf">The starting object.</param>
        /// <returns></returns>
        public static IEnumerable<DependencyObject> GetVisualAncestry(this DependencyObject? leaf)
        {
            while (leaf is not null)
            {
                yield return leaf;
                leaf = leaf is Visual or Visual3D
                    ? VisualTreeHelper.GetParent(leaf)
                    : LogicalTreeHelper.GetParent(leaf);
            }
        }

        /// <summary>
        /// Tries to find and returns a visual ancestor, starting at the leaf.
        /// <para>If element is not of <see cref="Visual"/> or <see cref="Visual3D"/> the logical ancestry is used.</para>
        /// </summary>
        /// <param name="leaf">The starting object.</param>
        /// <returns></returns>
        public static T? GetVisualAncestor<T>(this DependencyObject? leaf)
            where T : DependencyObject
        {
            while (leaf is not null)
            {
                if (leaf is T ancestor)
                {
                    return ancestor;
                }

                leaf = leaf is Visual or Visual3D
                    ? VisualTreeHelper.GetParent(leaf)
                    : LogicalTreeHelper.GetParent(leaf);
            }

            return default(T);
        }

        /// <summary>
        /// Finds a Child of a given item in the visual tree. 
        /// </summary>
        /// <param name="parent">A direct parent of the queried item.</param>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="childName">x:Name or Name of child. </param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, 
        /// a null parent is being returned.</returns>
        public static T? FindChild<T>(this DependencyObject? parent, string? childName = null)
            where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent is null)
            {
                return null;
            }

            T? foundChild = null;

            var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                // If the child is not of the request child type child
                if (child is not T currentChild)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);
                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild is not null)
                    {
                        break;
                    }
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    // If the child's name is set for search
                    if (currentChild is IFrameworkInputElement frameworkInputElement && frameworkInputElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = currentChild;
                        break;
                    }

                    // recursively drill down the tree
                    foundChild = FindChild<T>(currentChild, childName);
                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild is not null)
                    {
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = currentChild;
                    break;
                }
            }

            return foundChild;
        }

        /// <summary>
        /// Analyzes both visual and logical tree in order to find all elements of a given
        /// type that are descendants of the <paramref name="source"/> item.
        /// </summary>
        /// <typeparam name="T">The type of the queried items.</typeparam>
        /// <param name="source">The root element that marks the source of the search. If the
        /// source is already of the requested type, it will not be included in the result.</param>
        /// <param name="forceUsingTheVisualTreeHelper">Sometimes it's better to search in the VisualTree (e.g. in tests)</param>
        /// <returns>All descendants of <paramref name="source"/> that match the requested type.</returns>
        public static IEnumerable<T> FindChildren<T>(this DependencyObject? source, bool forceUsingTheVisualTreeHelper = false)
            where T : DependencyObject
        {
            if (source is not null)
            {
                var childObjects = GetChildObjects(source, forceUsingTheVisualTreeHelper);
                foreach (var child in childObjects)
                {
                    // analyze if children match the requested type
                    if (child is T childToFind)
                    {
                        yield return childToFind;
                    }

                    // recurse tree
                    foreach (T descendant in FindChildren<T>(child, forceUsingTheVisualTreeHelper))
                    {
                        yield return descendant;
                    }
                }
            }
        }

        /// <summary>
        /// This method is an alternative to WPF's
        /// <see cref="VisualTreeHelper.GetChild"/> method, which also
        /// supports content elements. Keep in mind that for content elements,
        /// this method falls back to the logical tree of the element.
        /// </summary>
        /// <param name="parent">The item to be processed.</param>
        /// <param name="forceUsingTheVisualTreeHelper">Sometimes it's better to search in the VisualTree (e.g. in tests)</param>
        /// <returns>The submitted item's child elements, if available.</returns>
        public static IEnumerable<DependencyObject> GetChildObjects(this DependencyObject? parent, bool forceUsingTheVisualTreeHelper = false)
        {
            if (parent is not null)
            {
                if (!forceUsingTheVisualTreeHelper && (parent is ContentElement || parent is FrameworkElement))
                {
                    // use the logical tree for content / framework elements
                    foreach (var obj in LogicalTreeHelper.GetChildren(parent))
                    {
                        if (obj is DependencyObject dependencyObject)
                        {
                            yield return dependencyObject;
                        }
                    }
                }
                else if (parent is Visual || parent is Visual3D)
                {
                    // use the visual tree per default
                    int count = VisualTreeHelper.GetChildrenCount(parent);
                    for (int i = 0; i < count; i++)
                    {
                        yield return VisualTreeHelper.GetChild(parent, i);
                    }
                }
            }
        }

        /// <summary>
        /// Tries to locate a given item within the visual tree,
        /// starting with the dependency object at a given position. 
        /// </summary>
        /// <typeparam name="T">The type of the element to be found
        /// on the visual tree of the element at the given location.</typeparam>
        /// <param name="reference">The main element which is used to perform
        /// hit testing.</param>
        /// <param name="point">The position to be evaluated on the origin.</param>
        public static T? TryFindFromPoint<T>(UIElement reference, Point point)
            where T : DependencyObject
        {
            if (!(reference.InputHitTest(point) is DependencyObject element))
            {
                return null;
            }

            if (element is T theObject)
            {
                return theObject;
            }

            return TryFindParent<T>(element);
        }

        public static bool IsDescendantOf(this DependencyObject node, DependencyObject reference)
        {
            DependencyObject? currentNode = node;

            while (currentNode is not null)
            {
                if (currentNode == reference)
                {
                    return true;
                }

                if (currentNode is Popup popup)
                {
                    // Try the Parent of the Popup
                    // Otherwise fall back to placement target
                    currentNode = popup.Parent ?? popup.PlacementTarget;
                }
                else
                {
                    // Otherwise walk tree
                    currentNode = currentNode.GetParentObject();
                }
            }

            return false;
        }

        /// <summary>
        /// This method is an alternative to WPF's
        /// <see cref="VisualTreeHelper.GetParent"/> method, which also supports content elements. Keep in mind that for content element,
        /// this method falls back to the logical tree of the element!
        /// </summary>
        /// <param name="child">The item to be processed.</param>
        /// <returns>The submitted item's parent, if available. Otherwise
        /// null.</returns>
        public static DependencyObject? GetParentObject(this DependencyObject? child)
        {
            if (child is null)
            {
                return null;
            }

            // handle content elements separately
            if (child is ContentElement contentElement)
            {
                DependencyObject parent = ContentOperations.GetParent(contentElement);
                if (parent is not null)
                {
                    return parent;
                }

                return contentElement is FrameworkContentElement fce ? fce.Parent : null;
            }

            var childParent = VisualTreeHelper.GetParent(child);
            if (childParent is not null)
            {
                return childParent;
            }

            // also try searching for parent in framework elements (such as DockPanel, etc)
            if (child is FrameworkElement frameworkElement)
            {
                DependencyObject parent = frameworkElement.Parent;
                if (parent is not null)
                {
                    return parent;
                }
            }

            return null;
        }
    }
}
