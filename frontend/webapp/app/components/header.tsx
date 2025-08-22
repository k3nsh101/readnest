import { Bell, User } from "lucide-react";

export default function Header() {
  return (
    <header className="flex justify-end items-center px-6 py-3">
      <div className="flex items-center space-x-6">
        <div className="flex items-center space-x-2 cursor-pointer p-2 rounded">
          <User className="w-6 h-6 text-gray-600" />
          <span className="font-medium">Samudu</span>
        </div>

        <button className="relative text-gray-600 cursor-pointer  hover:text-gray-800">
          <Bell className="w-6 h-6" />
          <span className="absolute -top-1 -right-1 bg-red-500 text-white text-xs rounded-full px-1">
            3
          </span>
        </button>
      </div>
    </header>
  );
}
