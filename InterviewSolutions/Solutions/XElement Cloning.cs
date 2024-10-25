/*
Specific to C# and XML processing. When working with XElement objects in C#, implicit 
cloning can happen in certain scenarios, which might lead to unexpected behavior if 
you're not aware of it.
The key points about implicit XElement cloning are:

When you add an XElement as a child to another XElement, it's automatically cloned if it 
already has a parent
This prevents the same node from appearing in multiple places in the XML tree
The cloning is deep - it copies the entire subtree