"use client";
import Link from "next/link";
import {
  BookOpen,
  PlusSquare,
  Layers,
  Activity,
  Calendar,
  BarChart3,
  Settings,
  ChevronRight,
  ChevronLeft,
} from "lucide-react";
import { useState } from "react";

export default function Sidebar() {
  const [collapsed, setCollapsed] = useState(false);
  const [showLabels, setShowLabels] = useState(true);

  const toggleSidebar = () => setCollapsed(!collapsed);

  return (
    <aside
      className={`h-screen bg-gray-900 text-white flex flex-col transition-all duration-300 ${
        collapsed ? "w-20" : "w-54"
      }`}
      onTransitionEnd={() => {
        if (!collapsed) setShowLabels(true);
        else setShowLabels(false);
      }}
    >
      <div className="flex justify-between">
        <div
          className={`h-16 flex items-center text-xl font-bold border-b border-gray-800
            ${collapsed ? "pl-5 pr-2" : "px-6"}`}
        >
          📚 {!collapsed && showLabels && <span>&nbsp; ReadNest</span>}
        </div>

        <div className="flex justify-end p-2">
          <button
            onClick={toggleSidebar}
            className="text-gray-400 hover:text-white"
          >
            {collapsed ? <ChevronRight size={20} /> : <ChevronLeft size={20} />}
          </button>
        </div>
      </div>

      <nav className="flex-1 px-4 py-6 space-y-2">
        <Link
          href="/books"
          className="flex items-center space-x-3 p-2 rounded-lg hover:bg-gray-800 transition"
        >
          <BookOpen className="w-5 h-5" />
          {!collapsed && showLabels && <span>My Books</span>}
        </Link>

        <Link
          href="/books/new"
          className="flex items-center space-x-3 p-2 rounded-lg hover:bg-gray-800 transition"
        >
          <PlusSquare className="w-5 h-5" />
          {!collapsed && showLabels && <span>Add Book</span>}
        </Link>

        <Link
          href="/genres"
          className="flex items-center space-x-3 p-2 rounded-lg hover:bg-gray-800 transition"
        >
          <Layers className="w-5 h-5" />
          {!collapsed && showLabels && <span>Genres</span>}
        </Link>

        <Link
          href="/habits"
          className="flex items-center space-x-3 p-2 rounded-lg hover:bg-gray-800 transition"
        >
          <Activity className="w-5 h-5" />
          {!collapsed && showLabels && <span>Habits</span>}
        </Link>

        <Link
          href="/schedule"
          className="flex items-center space-x-3 p-2 rounded-lg hover:bg-gray-800 transition"
        >
          <Calendar className="w-5 h-5" />
          {!collapsed && showLabels && <span>Schedule</span>}
        </Link>

        <Link
          href="/stats"
          className="flex items-center space-x-3 p-2 rounded-lg hover:bg-gray-800 transition"
        >
          <BarChart3 className="w-5 h-5" />
          {!collapsed && showLabels && <span>Statistics</span>}
        </Link>
      </nav>

      <div className="px-4 py-4 border-t border-gray-800">
        <Link
          href="/settings"
          className="flex items-center space-x-3 p-2 rounded-lg hover:bg-gray-800 transition"
        >
          <Settings className="w-5 h-5" />
          {!collapsed && showLabels && <span>Settings</span>}
        </Link>
      </div>
    </aside>
  );
}
